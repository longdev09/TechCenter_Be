using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using TechCenter.DTO.TaiLieu;
using TechCenter.Models;
using TechCenter.Services.Interface;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechCenter.Services
{
    public class TaiLieuService : ITaiLieuService
    {
        private readonly AppDbContext _context;
        private readonly IUploadAnhService _uploadService;
        public TaiLieuService(AppDbContext context, IUploadAnhService uploadService)
        {
            _context = context;
            _uploadService = uploadService;
        }


        public async Task ThemTaiLieuAsync(
            Tailieu tailieu,
            IFormFile? file,
            int? idLophoc = null,
            string folder = "tailieu")
        {
            if (tailieu == null)
                throw new ArgumentNullException(nameof(tailieu));

            tailieu.Tieudetl = (tailieu.Tieudetl ?? string.Empty).Trim();
            tailieu.Motatl = (tailieu.Motatl ?? string.Empty).Trim();
            tailieu.Ngaydangtl = DateTime.Now;

            if (file != null && file.Length > 0)
            {
                var baseName = string.IsNullOrWhiteSpace(tailieu.Tieudetl)
                    ? "tailieu"
                    : Path.GetFileNameWithoutExtension(tailieu.Tieudetl).Replace(" ", "_");

                var (url, _) = await _uploadService.UploadFileAsync(file, folder, baseName);
                tailieu.Urltailieu = url;
            }

            _context.Tailieus.Add(tailieu);

            // Gán 1 lớp học
            if (idLophoc.HasValue)
            {
                var lophoc = await _context.Lophocs
                    .FirstOrDefaultAsync(x => x.IdLophoc == idLophoc.Value);

                if (lophoc == null)
                    throw new Exception("Lớp học không tồn tại");

                tailieu.IdLophocs ??= new List<Lophoc>();
                tailieu.IdLophocs.Add(lophoc);
            }

            await _context.SaveChangesAsync();
        }




        public async Task<object> GetTaiLieuChoHocVienAsync(int idHocVien)
        {
            //1. Lấy danh sách lớp học mà học viên đã đăng ký
            var lopHocIds = await _context.Dangkylops
                .Where(d => d.IdHv == idHocVien)
                .Select(d => d.IdLophoc)
                .Distinct()
                .ToListAsync();

            if (!lopHocIds.Any())
                return new List<TaiLieuHocVienDTO>();

            //2. Lấy giáo viên mới nhất cho từng lớp 
            var latestPhanCong = await _context.Phancongs
                .Where(pc => lopHocIds.Contains(pc.IdLophoc))
                .GroupBy(pc => pc.IdLophoc)
                .Select(g => g.OrderByDescending(x => x.Ngayphancong).First())
                .ToListAsync();

            // Map theo lop → giáo viên
            var dictGiaoVien = latestPhanCong
                .ToDictionary(
                    x => x.IdLophoc,
                    x => x.IdGiaovienNavigation?.Hotengv
                );

            //3. Lấy tài liệu qua bảng many-to-many
            var data = await (
                from tl in _context.Tailieus.AsNoTracking()
                from lh in tl.IdLophocs
                where lopHocIds.Contains(lh.IdLophoc) && tl.IsPublic

                join kh in _context.Khoahocs.AsNoTracking()
                    on lh.IdKhoahoc equals kh.IdKhoahoc into khJoin
                from kh in khJoin.DefaultIfEmpty()

                select new
                {
                    tl.IdTailieu,
                    tl.Tieudetl,
                    tl.Motatl,
                    tl.Urltailieu,
                    tl.Ngaydangtl,
                    tl.IsPublic,
                    lh.IdLophoc,
                    TenKhoaHoc = kh != null ? kh.Tenkhoahoc : null,
                    IdKhoaHoc = kh != null ? (int?)kh.IdKhoahoc : null
                }
            ).ToListAsync();

            //4. Gán giáo viên sau cùng (EF không join được nên phải làm ngoài)
            var groupedResult = data
                .Select(x => new
                {
                    IdTailieu = x.IdTailieu,
                    Tieudetl = x.Tieudetl,
                    Motatl = x.Motatl,
                    Urltailieu = x.Urltailieu,
                    Ngaydangtl = x.Ngaydangtl,
                    IsPublic = x.IsPublic,
                    IdLophoc = x.IdLophoc,
                    TenKhoaHoc = x.TenKhoaHoc,
                    Hotengv = dictGiaoVien.ContainsKey(x.IdLophoc) ? dictGiaoVien[x.IdLophoc] : null
                })
                .GroupBy(x => x.TenKhoaHoc) // nhóm theo tên khóa học
                .Select(g => new
                {
                    TenKhoaHoc = g.Key,
                    TaiLieus = g.ToList()
                })
                .ToList();


            return groupedResult;
        }


        public async Task<List<TaiLieuByLopDTO>> GetTaiLieuByLopAsync(int idLophoc)
        {
            // latest assigned teacher for the class (optional)
            var hotenGV = await _context.Phancongs
                .Where(pc => pc.IdLophoc == idLophoc)
                .OrderByDescending(pc => pc.Ngayphancong)
                .Select(pc => pc.IdGiaovienNavigation != null ? pc.IdGiaovienNavigation.Hotengv : null)
                .FirstOrDefaultAsync();

            var list = await (
                from tl in _context.Tailieus.AsNoTracking()
                from lh in tl.IdLophocs
                where lh.IdLophoc == idLophoc
                join kh in _context.Khoahocs.AsNoTracking() on lh.IdKhoahoc equals kh.IdKhoahoc into khj
                from kh in khj.DefaultIfEmpty()
                select new TaiLieuByLopDTO
                {
                    IdTailieu = tl.IdTailieu,
                    Tieudetl = tl.Tieudetl,
                    Motatl = tl.Motatl,
                    Urltailieu = tl.Urltailieu,
                    Ngaydangtl = tl.Ngaydangtl,
                    IsPublic = tl.IsPublic,
                    IdLophoc = lh.IdLophoc,
                    TenKhoaHoc = kh != null ? kh.Tenkhoahoc : null,
                    Hotengv = hotenGV
                }
            ).ToListAsync();

            return list;
        }


    }
}
