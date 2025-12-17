using System;

namespace TechCenter.DTO.LichHoc
{
    public class LichHocChiTietGiaoVienTuan
    {
        public int IdGiaoVien { get; set; }
        public string? HoTenGiaoVien { get; set; }

        public int IdLop { get; set; }
        public int IdKhoaHoc { get; set; }
        public string? TenKhoaHoc { get; set; }

        public int IdLichHoc { get; set; }
        public int? Thu { get; set; }
        public int? GioBatDau { get; set; }
        public int? GioKetThuc { get; set; }

        public int IdLHCT { get; set; }
        public DateOnly NgayHoc { get; set; }
        public string? TieuDeBuoiHoc { get; set; }
        public string? UrlBuoiHoc { get; set; }
    }
}