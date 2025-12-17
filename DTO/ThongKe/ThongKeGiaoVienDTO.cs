using System;
using System.Collections.Generic;

namespace TechCenter.DTO.ThongKe
{
    public class DiemTrungBinhMonGvDTO
    {
        public int? IdKhoaHoc { get; set; }
        public string? TenKhoaHoc { get; set; }
        public double DiemTrungBinh { get; set; }
        public int SoBaiThiDaCham { get; set; }
    }

    public class DiemTheoBaiThiGvDTO
    {
        public int IdBaithi { get; set; }
        public string? TenBaiThi { get; set; }
        public string? TenKhoaHoc { get; set; }
        public DateTime NgayThiGanNhat { get; set; }
        public double? DiemTrungBinh { get; set; }
        public int SoBaiDaCham { get; set; }
    }

    public class TyLeChamDiemGvDTO
    {
        public int TongBaiKetQua { get; set; }     // t?ng bài (ketquathi) thu?c các l?p GV
        public int SoBaiDaCham { get; set; }       // TrangthaiCham = "DA_CHAM"
        public int SoBaiChoCham { get; set; }      // còn l?i
    }

    public class ThongKeGiaoVienDTO
    {
        public List<DiemTrungBinhMonGvDTO> DiemTrungBinhTheoMon { get; set; } = new();
        public List<DiemTheoBaiThiGvDTO> DiemTheoBaiThi { get; set; } = new();
        public TyLeChamDiemGvDTO TyLeChamDiem { get; set; } = new();
    }
}