using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.DangKyHoc;
using TechCenter.DTO.ThanhToan;
using TechCenter.Models;
using TechCenter.Services.Interface;

namespace TechCenter.Services
{
    public class DangKyHocService : IDangKyHocService
    {
        private readonly AppDbContext _context;
        private readonly IThanhToanService _thanhToanService;
        public DangKyHocService(AppDbContext context, IThanhToanService thanhToanService)
        {
            _context = context;
            _thanhToanService = thanhToanService;
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

            if (dto.thanhToanDTO != null)
            {
                // gán IdDangky trong DTO thanh toán bằng IdDangky vừa tạo
                dto.thanhToanDTO.IdDangky = entity.IdDangky;
                await _thanhToanService.CreateAsync(dto.thanhToanDTO);
            }

            return entity;
        }

        // get thong tin lich hoc cho hoc vien 



    }
}
