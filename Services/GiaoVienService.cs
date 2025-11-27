using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.GiaoVien;
using TechCenter.Models;
using TechCenter.Services.Interface;

namespace TechCenter.Services
{
    public class GiaoVienService : IGiaoVienService
    {
        private readonly AppDbContext _context;
        public GiaoVienService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GiaoVienDTO> CreateHocVien(GiaoVienDTO gv)
        {

            var newGv = new Giaovien
            {
                IdTaikhoan = gv.idTaiKhoan ?? 0,
                Hotengv = gv.hoTenGv ?? string.Empty,
                Gioitinhgv = gv.gioiTinhGv,
                Ngaysinhgv = gv.ngaySinhGv,
                Diachigv = gv.diaChiGv,
            };
            _context.Giaoviens.Add(newGv);
            await _context.SaveChangesAsync();
            return gv;
        }
    }
}
