using TechCenter.DTO;

namespace TechCenter.Services.Interface
{
    public interface ITaiKhoanService
    {
        Task<object> CreateTaiKhoan(string tenDangNhap, string matKhau, string email, int vaiTro, string tenNguoiDung);
        Task<object> Login(DangNhapDTO dangNhapDTO);
    }
}
