using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.HocVien;
using TechCenter.Models;
using TechCenter.Services.Interface;

namespace TechCenter.Services
{
    public class HocVienService : IHocVienService
    {
        private readonly AppDbContext _context;
        private readonly IUploadAnhService _uploadAnhService;
        public HocVienService(AppDbContext context, IUploadAnhService uploadAnhService)
        {
            _context = context;
            _uploadAnhService = uploadAnhService;
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

        public async Task<HocVienDTO?> GetByIdTaiKhoanAsync(int idTaiKhoan)
        {
            return await _context.Hocviens
                .AsNoTracking()
                .Where(h => h.IdTaikhoan == idTaiKhoan)
                .Select(h => new HocVienDTO
                {
                    idTaiKhoan = h.IdTaikhoan,
                    idHocVien = h.IdHocvien,
                    hoTenHv = h.Hotenhv,
                    gioiTinhHv = h.Gioitinhhv,
                    ngaySinhHv = h.Ngaysinhhv,
                    diaChiHv = h.Diachihv
                })
                .FirstOrDefaultAsync();
        }

        public  async Task<HocVienByIdDTO?> GetHocVienByIdAsync(int idHocVien)
        {
            return await _context.Hocviens
                .AsNoTracking()
                .Where(h => h.IdHocvien == idHocVien)
                .Select(h => new HocVienByIdDTO
                {
                    hoTenHv = h.Hotenhv,
                    tenDangNhap = h.IdTaikhoanNavigation.Tendangnhap,
                    gioiTinhHv = h.Gioitinhhv,
                    diaChiHv = h.Diachihv,
                    hinhAnhHv = null,
                    email = h.IdTaikhoanNavigation.Email,
                    ngaySinhHv = h.Ngaysinhhv,
                    anhHv = h.AnhHv

                })
                .FirstOrDefaultAsync();
        }


        // Sửa thông tin học viên + (tùy chọn) cập nhật tên đăng nhập / email trong tài khoản liên quan
        public async Task<HocVienByIdDTO?> UpdateHocVienAsync(UpdateHocVienDTO dto)
        {
            var hocVien = await _context.Hocviens
                .Include(h => h.IdTaikhoanNavigation) // để cập nhật thông tin tài khoản nếu cần
                .FirstOrDefaultAsync(h => h.IdHocvien == dto.idHocVien);

            if (hocVien == null)
                return null;

     

            // Cập nhật các trường học viên nếu có trong DTO
            if (!string.IsNullOrWhiteSpace(dto.hoTenHv) && dto.hoTenHv != hocVien.Hotenhv)
            {
                hocVien.Hotenhv = dto.hoTenHv;
           
            }

            if (!string.IsNullOrWhiteSpace(dto.gioiTinhHv) && dto.gioiTinhHv != hocVien.Gioitinhhv)
            {
                hocVien.Gioitinhhv = dto.gioiTinhHv;
              
            }

            if (!string.IsNullOrWhiteSpace(dto.diaChiHv) && dto.diaChiHv != hocVien.Diachihv)
            {
                hocVien.Diachihv = dto.diaChiHv;
              
            }

            if (dto.ngaySinhHv.HasValue && dto.ngaySinhHv != hocVien.Ngaysinhhv)
            {
                hocVien.Ngaysinhhv = dto.ngaySinhHv.Value;
                
            }

            // Xử lý upload ảnh chỉ khi có file mới
            if (dto.anhHv != null && dto.anhHv.Length >0)
            {
                try
                {
                    var uploadResult = await _uploadAnhService.UploadImageAsync(dto.anhHv, "hocvien", hocVien.IdHocvien.ToString());
                    if (!string.IsNullOrWhiteSpace(uploadResult.Url))
                    {
                        hocVien.AnhHv = uploadResult.Url;

                    }
                }
                catch (Exception ex)
                {
                    // Nếu upload thất bại, ném ra để middleware xử lý hoặc trả lỗi
                    throw new Exception("Lỗi khi upload ảnh học viên: " + ex.Message, ex);
                }
            }

            await _context.SaveChangesAsync();

            // Trả về DTO đã cập nhật (lấy ảnh từ entity)
            var updated = new HocVienByIdDTO
            {
                hoTenHv = hocVien.Hotenhv,
                tenDangNhap = hocVien.IdTaikhoanNavigation != null ? hocVien.IdTaikhoanNavigation.Tendangnhap : null,
                email = hocVien.IdTaikhoanNavigation != null ? hocVien.IdTaikhoanNavigation.Email : null,
                gioiTinhHv = hocVien.Gioitinhhv,
                diaChiHv = hocVien.Diachihv,
                hinhAnhHv = hocVien.AnhHv,
                ngaySinhHv = hocVien.Ngaysinhhv
            };

            return updated;
        }


    } 
}
