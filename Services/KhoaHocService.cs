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
                    MoTa = k.Motakh,
                    HocPhi = k.Hocphi,
                    AnhDaiDien = k.Anhdaidien,
                    TenCapDo = k.IdcapdokhoahocNavigation != null ? k.IdcapdokhoahocNavigation.TenCapDo : null,
                    MauSac = k.IdcapdokhoahocNavigation != null ? k.IdcapdokhoahocNavigation.MauSac : null
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
                    MoTa = k.Motakh,
                    HocPhi = k.Hocphi,
                    AnhDaiDien = k.Anhdaidien,
                    TenCapDo = k.IdcapdokhoahocNavigation != null ? k.IdcapdokhoahocNavigation.TenCapDo : null,
                    MauSac = k.IdcapdokhoahocNavigation != null ? k.IdcapdokhoahocNavigation.MauSac : null
                })
                .FirstOrDefaultAsync();
        }

        //public async Task<KhoaHocDTO> GetKhoaHocByID(int id)
        //{
        //   return await _context.Khoahocs
        //    .Where(k => k.IdKhoahoc == id)
        //    .Select(k => new KhoaHocDTO
        //    {
        //        IdKhoahoc = k.IdKhoahoc,
        //        Tenkhoahoc = k.Tenkhoahoc,
        //        Motakh = k.Motakh,
        //        Hocphi = k.Hocphi,
        //        Anhdaidien = k.Anhdaidien
        //    })
        //    .FirstOrDefaultAsync();
        //}

    }
}
