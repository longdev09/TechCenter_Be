using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.TaiLieu;
using TechCenter.Models;
using TechCenter.Services.Interface;

namespace TechCenter.Services
{
    public class TaiLieuService : ITaiLieuService
    {
        private readonly AppDbContext _context;
        public TaiLieuService(AppDbContext context)
        {
            _context = context;
        }


        public async Task ThemTaiLieu (Tailieu tailieu)
        {
            _context.Tailieus.Add(tailieu);
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

       
    }
}
