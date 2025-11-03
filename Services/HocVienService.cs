using Microsoft.EntityFrameworkCore;
using TechCenter.DTO;
using TechCenter.Models;
using TechCenter.Services.Interface;

namespace TechCenter.Services
{
    public class HocVienService : IHocVienService
    {
        private readonly AppDbContext _context;
        public HocVienService(AppDbContext context)
        {
            _context = context;
        }

        // Thêm mới học viên
        public async Task<HocVienDTO> CreateHocVien(HocVienDTO hocvien)
        {

            var newHocVien = new Hocvien
            {
                IdTaikhoan = hocvien.idTaiKhoan ?? 0,
                Hotenhv = hocvien.hoTenHv ?? string.Empty,
                Gioitinhhv = hocvien.gioiTinhHv,
                Ngaysinhhv = hocvien.ngaySinhHv,
                Diachihv = hocvien.diaChiHv
            };
            _context.Hocviens.Add(newHocVien);
            await _context.SaveChangesAsync();
            return hocvien;
        }

        // Lấy danh sách tất cả học viên
        public async Task<List<Hocvien>> GetAllAsync()
        {
            return await _context.Hocviens.ToListAsync();
        }

        // Lấy học viên theo Id
        public async Task<Hocvien?> GetByIdAsync(int id)
        {
            return await _context.Hocviens.FindAsync(id);
        }

        // Cập nhật thông tin học viên
        public async Task<bool> UpdateAsync(Hocvien hocvien)
        {
            var existing = await _context.Hocviens.FindAsync(hocvien.IdHocvien);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(hocvien);
            await _context.SaveChangesAsync();
            return true;
        }

        // Xóa học viên
        public async Task<bool> DeleteAsync(int id)
        {
            var hocvien = await _context.Hocviens.FindAsync(id);
            if (hocvien == null)
                return false;

            _context.Hocviens.Remove(hocvien);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
