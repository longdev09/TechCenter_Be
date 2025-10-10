namespace TechCenter.DTO
{
    public class TaiKhoanDTO
    {
        public int IdTaikhoan { get; set; }
        public int IdVaitro { get; set; }
        public string Tendangnhap { get; set; } = null!;
        public string Matkhau { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Sodienthoai { get; set; }
    }
}
