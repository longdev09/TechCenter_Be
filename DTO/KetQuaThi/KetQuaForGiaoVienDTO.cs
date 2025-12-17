using System;

namespace TechCenter.DTO.KetQuaThi
{
    public class KetQuaForGiaoVienDTO
    {
        public int IdKetqua { get; set; }
        public int IdBaithi { get; set; }
        public string? Tieude { get; set; }
        public int? IdLop { get; set; }
        public string? TenKhoaHoc { get; set; }

        public int IdHocvien { get; set; }
        public string? TenHocVien { get; set; }

        public int LanThi { get; set; }
        public string? Trangthai { get; set; }
        public string? TrangthaiCham { get; set; }
        public double? Tongdiem { get; set; }
        public DateTime Ngaythi { get; set; }
    }
}