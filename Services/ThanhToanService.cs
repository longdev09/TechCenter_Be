using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.ThanhToan;
using TechCenter.Models;
using TechCenter.Services.Interface;

namespace TechCenter.Services
{
    public class ThanhToanService : IThanhToanService
    {
        private readonly AppDbContext _context;
        public ThanhToanService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Thanhtoan>> GetAllAsync()
        {
            return await _context.Thanhtoans.AsNoTracking().ToListAsync();
        }

        public async Task<Thanhtoan?> GetByIdAsync(int id)
        {
            return await _context.Thanhtoans.FindAsync(id);
        }

        public async Task<Thanhtoan> CreateAsync(InsertThanhToanDTO dto)
        {
            var entity = new Thanhtoan
            {
                IdDangky = dto.IdDangky,
                Sotien = dto.Sotien,
                Phuongthuctt = dto.Phuongthuctt,
                Magiaodich = dto.Magiaodich,
                Trangthai = dto.Trangthai,
                Ngaytt = dto.Ngaytt ?? DateTime.Now,
                Noidung = dto.Noidung
            };

            _context.Thanhtoans.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(Thanhtoan thanhtoan)
        {
            var existing = await _context.Thanhtoans.FindAsync(thanhtoan.IdThanhtoan);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(thanhtoan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Thanhtoans.FindAsync(id);
            if (existing == null) return false;
            _context.Thanhtoans.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
