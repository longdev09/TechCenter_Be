using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.KhoaHoc;
using TechCenter.Models;
using TechCenter.Services.Interface;

namespace TechCenter.Services
{
    public class KhoaHocService : IKhoaHocService
    {
        private readonly AppDbContext _context;
        public KhoaHocService(AppDbContext context)
        {
            _context = context;
        }

        // lấy danh sách các khoa học
        public async Task<List<KhoaHocDTO>> GetAllKhoaHocAsync()
        {
            return await _context.Khoahocs
            .AsNoTracking()
                .Select(k => new KhoaHocDTO
                {
                    IdKhoaHoc = k.IdKhoahoc,
                    TenKhoaHoc = k.Tenkhoahoc,
                    HocPhi = k.Hocphi,
                    AnhDaiDien = k.Anhdaidien,
                    TenCapDo = k.IdcapdokhoahocNavigation != null ? k.IdcapdokhoahocNavigation.TenCapDo : null,
                    MauSac = k.IdcapdokhoahocNavigation != null ? k.IdcapdokhoahocNavigation.MauSac : null,
                    MoTa = k.Motakh,
                    KetQuaDatDuoc = k.Ketquadatduoc,
                    NoiDungKhoaHoc = k.Noidungkhoahoc,
                    TenLoaiKyNang = k.IdLoaikynangNavigation != null ? k.IdLoaikynangNavigation.Tenloaikynang : null
                })
                .ToListAsync();
        }

        // Lấy khóa học theo id
        public async Task<KhoaHocDTO?> GetKhoaHocById(int id)
        {
            return await _context.Khoahocs
                .AsNoTracking()
                .Where(k => k.IdKhoahoc == id)
                .Select(k => new KhoaHocDTO
                {
                    IdKhoaHoc = k.IdKhoahoc,
                    TenKhoaHoc = k.Tenkhoahoc,
                    HocPhi = k.Hocphi,
                    AnhDaiDien = k.Anhdaidien,
                    TenCapDo = k.IdcapdokhoahocNavigation != null ? k.IdcapdokhoahocNavigation.TenCapDo : null,
                    MauSac = k.IdcapdokhoahocNavigation != null ? k.IdcapdokhoahocNavigation.MauSac : null,
                    MoTa = k.Motakh,
                    KetQuaDatDuoc = k.Ketquadatduoc,
                    NoiDungKhoaHoc = k.Noidungkhoahoc,
                    TenLoaiKyNang = k.IdLoaikynangNavigation != null ? k.IdLoaikynangNavigation.Tenloaikynang : null
                })
                .FirstOrDefaultAsync();
        }


        public async Task<List<KhoaHocDTO>> GetKhoaHocByIdHocVien(int idHocVien)
        {
            // Join Dangkylop -> Lophoc -> Khoahoc cho học viên có id
            var truyVan = from dangKy in _context.Dangkylops.AsNoTracking()
                        where dangKy.IdHv == idHocVien
                        join lop in _context.Lophocs.AsNoTracking() on dangKy.IdLophoc equals lop.IdLophoc
                        join khoaHoc in _context.Khoahocs.AsNoTracking() on lop.IdKhoahoc equals khoaHoc.IdKhoahoc
                        select new KhoaHocDTO
                        {
                            IdKhoaHoc = khoaHoc.IdKhoahoc,
                            TenKhoaHoc = khoaHoc.Tenkhoahoc,
                            HocPhi = khoaHoc.Hocphi,
                            AnhDaiDien = khoaHoc.Anhdaidien,
                            TenCapDo = khoaHoc.IdcapdokhoahocNavigation != null ? khoaHoc.IdcapdokhoahocNavigation.TenCapDo : null,
                            MauSac = khoaHoc.IdcapdokhoahocNavigation != null ? khoaHoc.IdcapdokhoahocNavigation.MauSac : null,
                            MoTa = khoaHoc.Motakh,
                            KetQuaDatDuoc = khoaHoc.Ketquadatduoc,
                            NoiDungKhoaHoc = khoaHoc.Noidungkhoahoc,
                            TenLoaiKyNang = khoaHoc.IdLoaikynangNavigation != null ? khoaHoc.IdLoaikynangNavigation.Tenloaikynang : null
                        };

            var danhSach = await truyVan.ToListAsync();
            var ketQua = danhSach.GroupBy(x => x.IdKhoaHoc).Select(g => g.First()).ToList();
            return ketQua;
        }

    }
}
