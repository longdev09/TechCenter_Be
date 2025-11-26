using TechCenter.DTO.HoaDon;
using TechCenter.DTO.ThanhToan;

namespace TechCenter.DTO.DangKyHoc
{
    public class InsertDangKyHocDTO
    {
        public int? idHV { get; set; }
        public int? idLopHoc { get; set; }
        public DateTime ngayDangKy { get; set; }
        public float? soTienGiam { get; set; }
        public float? tongTien { get; set; }
        public InsertHoaDonDTO? InsertHoaDonDTO { get; set; }


    }
}
