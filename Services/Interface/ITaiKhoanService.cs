using TechCenter.DTO;

namespace TechCenter.Services.Interface
{
    public interface ITaiKhoanService
    {
        Task<object> CreateTaiKhoan(string tenDangNhap, string matKhau, string sdt, string email, int vaiTro);
        Task<object> Login(DangNhapDTO dangNhapDTO);
    }
}
