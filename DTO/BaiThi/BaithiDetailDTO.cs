using System;

namespace TechCenter.DTO.BaiThi
{
    public class BaithiDetailDTO
    {
        // baithi
        public int IdBaithi { get; set; }
        public string? Tieude { get; set; }
        public int? ThoiLuong { get; set; }
        public string? Mota { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? IdLoaiBaithi { get; set; }
        public string? TenLoai { get; set; }
        public int? NguoiTao { get; set; }
        public DateTime? NgayTao { get; set; }

        // lop
        public int? IdLop { get; set; }
        public int? IdKhoaHoc { get; set; }
        public int? SiSoHienTai { get; set; }
        public int? SiSoToiDa { get; set; }
        public DateTime? NgayKhaiGiang { get; set; }
        public DateTime? NgayBatDauLop { get; set; }
        public DateTime? NgayKetThucLop { get; set; }

        // khoa hoc
        public string? TenKhoaHoc { get; set; }
        public decimal? HocPhi { get; set; }
        public string? AnhDaiDien { get; set; }
    }
}