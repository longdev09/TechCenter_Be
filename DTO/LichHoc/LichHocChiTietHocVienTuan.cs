using Microsoft.EntityFrameworkCore;

namespace TechCenter.DTO.LichHoc
{
    [Keyless]
    public class LichHocChiTietHocVienTuan
    {
        public int? IdHv { get; set; }
        public string? HoTenHocVien { get; set; }  // Fix type: HoTenHocVien should be string
        public int? IdLop { get; set; }
        public int? IdKhoaHoc { get; set; }
        public string? TenKhoaHoc { get; set; }
        public int? IdLichHoc { get; set; }
        public int? Thu { get; set; }
        // giờ bắt đầu/ kết thúc (lấy hour để đơn giản) 
        public int? GioBatDau { get; set; }  // keep int? but add comments
        public int? GioKetThuc { get; set; }  // keep int? but add comments
        public int? IdLHCT { get; set; }
        public DateOnly? NgayHoc { get; set; }
        public string? TieuDeBuoiHoc { get; set; }
        public string? UrlBuoiHoc { get; set; }
    }
}
