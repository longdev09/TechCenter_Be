using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.DangKyHoc;
using TechCenter.DTO.HoaDon;
using TechCenter.DTO.ThanhToan;
using TechCenter.Models;
using TechCenter.Services.Interface;

namespace TechCenter.Services
{
    public class DangKyHocService : IDangKyHocService
    {
        private readonly AppDbContext _context;
        private readonly IHoaDonService _hoaDonService;
        public DangKyHocService(AppDbContext context, IHoaDonService hoaDonService)
        {
            _context = context;
           _hoaDonService = hoaDonService;
        }

        // Học viên đăng ký học khóa học
        public async Task<Dangkylop> CreateDangKyLopAsync(InsertDangKyHocDTO dto)
        {

            var entity = new Dangkylop
            {
                IdHv = dto.idHV.Value,
                IdLophoc = dto.idLopHoc.Value,
                Ngaydangky = dto.ngayDangKy == default ? DateTime.Now : dto.ngayDangKy,
                Sotiengiam = Convert.ToDecimal(dto.soTienGiam ??0f),
                Tongtien = Convert.ToDecimal(dto.tongTien ??0f)
            };

            _context.Dangkylops.Add(entity);
            await _context.SaveChangesAsync();

            if (dto.InsertHoaDonDTO != null)
            {
                
                await _hoaDonService.CreateAsync(new InsertHoaDonDTO
                {
                   maHd = dto.InsertHoaDonDTO.maHd,
                   IdHocvien = dto.idHV,
                   IdLophoc = dto.idLopHoc,
                   Tongtien = dto.InsertHoaDonDTO.Tongtien,
                   Ghichu = dto.InsertHoaDonDTO.Ghichu,
                   Trangthai = dto.InsertHoaDonDTO.Trangthai
                });
            }

            return entity;
        }

        // get thong tin lich hoc cho hoc vien 



    }
}
