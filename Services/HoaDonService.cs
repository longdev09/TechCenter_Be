using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.HoaDon;
using TechCenter.Models;
using TechCenter.Services.Interface;
using TechCenter.Helpers;

namespace TechCenter.Services
{
    public class HoaDonService : IHoaDonService
    {
        private readonly AppDbContext _context;
        public HoaDonService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<HoaDonDTO>> GetAllAsync()
        {
            return await _context.Hoadons
                .AsNoTracking()
                .Select(h => new HoaDonDTO
                {
                    Idhoadon = h.Idhoadon,
                    Mahoadon = h.Mahoadon,
                    IdHocvien = h.IdHocvien,
                    IdLophoc = h.IdLophoc,
                    Ngaytao = h.Ngaytao,
                   
                    Trangthai = h.Trangthai,
                    Ghichu = h.Ghichu,
                    TenHocVien = h.IdHocvienNavigation != null ? h.IdHocvienNavigation.Hotenhv : null
                })
                .ToListAsync();
        }

        public async Task<HoaDonDTO?> GetByIdAsync(int id)
        {
            var h = await _context.Hoadons
                .AsNoTracking()
                .Where(x => x.Idhoadon == id)
                .Select(h => new HoaDonDTO
                {
                    Idhoadon = h.Idhoadon,
                    Mahoadon = h.Mahoadon,
                    IdHocvien = h.IdHocvien,
                    IdLophoc = h.IdLophoc,
                    Ngaytao = h.Ngaytao,
                  
                    Trangthai = h.Trangthai,
                    Ghichu = h.Ghichu,
                    TenHocVien = h.IdHocvienNavigation != null ? h.IdHocvienNavigation.Hotenhv : null
                })
                .FirstOrDefaultAsync();

            // Fetch TenKhoaHoc with a separate query to avoid expression tree limitations
            if (h != null)
            {
                var tenKhoa = await _context.Hoadons
                    .Where(x => x.Idhoadon == id)
                    .Join(_context.Lophocs, hd => hd.IdLophoc, lp => lp.IdLophoc, (hd, lp) => new { lp.IdKhoahoc })
                    .Join(_context.Khoahocs, x => x.IdKhoahoc, kh => kh.IdKhoahoc, (x, kh) => kh.Tenkhoahoc)
                    .FirstOrDefaultAsync();

                h.TenKhoaHoc = tenKhoa;
            }

            return h;
        }

        public async Task<HoaDonDTO> CreateAsync(InsertHoaDonDTO dto)
        {
           
        

            var entity = new Hoadon
            {
                Mahoadon = dto.maHd,
                IdHocvien = dto.IdHocvien.Value,
                IdLophoc = dto.IdLophoc.Value,
                Ngaytao = DateTime.Now,
                Tongtien = dto.Tongtien.Value,
                Trangthai = string.IsNullOrEmpty(dto.Trangthai) ? "CHUATHANHTOAN" : dto.Trangthai,
                Ghichu = dto.Ghichu
            };

            _context.Hoadons.Add(entity);
            await _context.SaveChangesAsync();

            // fetch TenHocVien và TenKhoaHoc cùng lúc
            var result = await _context.Hoadons
                .Where(hd => hd.Idhoadon == entity.Idhoadon)
                .Select(hd => new HoaDonDTO
                {
                    Idhoadon = hd.Idhoadon,
                    Mahoadon = hd.Mahoadon,
                    IdHocvien = hd.IdHocvien,
                    IdLophoc = hd.IdLophoc,
                    Ngaytao = hd.Ngaytao,
                 
                    Trangthai = hd.Trangthai,
                    Ghichu = hd.Ghichu,
                    TenHocVien = hd.IdHocvienNavigation != null ? hd.IdHocvienNavigation.Hotenhv : null,
                    TenKhoaHoc = hd.IdLophocNavigation != null && hd.IdLophocNavigation.IdKhoahocNavigation != null
                        ? hd.IdLophocNavigation.IdKhoahocNavigation.Tenkhoahoc
                        : null
                })
                .FirstOrDefaultAsync();

            return result;
        }
        public async Task<HoaDonDTO?> UpdateAsync(int id, UpdateHoaDonDTO dto)
        {
            var entity = await _context.Hoadons.FindAsync(id);
            if (entity == null) return null;

            if (dto.Tongtien.HasValue) entity.Tongtien = dto.Tongtien.Value;
            if (!string.IsNullOrWhiteSpace(dto.Trangthai)) entity.Trangthai = dto.Trangthai;
            if (!string.IsNullOrWhiteSpace(dto.Ghichu)) entity.Ghichu = dto.Ghichu;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Hoadons.FindAsync(id);
            if (entity == null) return false;
            _context.Hoadons.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
