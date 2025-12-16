using Microsoft.EntityFrameworkCore;

namespace TechCenter.DTO.BaiThi
{
    [Keyless]
    public class BaiThiLopHocDTo
    {
        public int IdBaithi { get; set; }
        public int? IdLop { get; set; }
        public int? IdKhoahoc { get; set; }
        public string? TenKhoaHoc { get; set; }
        public string? Tieude { get; set; }
        public int? Loai { get; set; }
        public int? Thoiluong { get; set; }
        public DateTime? Ngaytao { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
    }
}
