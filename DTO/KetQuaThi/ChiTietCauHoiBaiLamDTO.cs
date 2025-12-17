// DTO/KetQuaThi/ChiTietCauHoiBaiLamDTO.cs
using System;
using System.Collections.Generic;

namespace TechCenter.DTO.KetQuaThi
{
    public class ChiTietCauHoiBaiLamDTO
    {
        public int IdCauHoi { get; set; }
        public int? IdLoaiCauHoi { get; set; }
        public string? LoaiCauHoi { get; set; }
        public int? Stt { get; set; }
        public decimal? Diem { get; set; }
        public string? MucDo { get; set; }
        public string? NoiDungCauHoi { get; set; }

        // Trắc nghiệm
        public string? DapAnChon { get; set; }   // đáp án HV chọn (A/B/C/…)
        public bool? IsDung { get; set; }        // đúng/sai với câu chọn
        public double? DiemTracNghiem { get; set; }

        // Danh sách đáp án trắc nghiệm
        public List<DapAnTracNghiemInKetQuaDTO>? DapAnTracNghiems { get; set; }

        // Tự luận
        public string? NoiDungTuLuan { get; set; }
        public double? DiemGv { get; set; }
        public string? NhanXetGv { get; set; }
        public DateTime? NgayCham { get; set; }
    }

    public class DapAnTracNghiemInKetQuaDTO
    {
        public int IdDapAn { get; set; }
        public string? Ma { get; set; }          // A/B/C/...
        public string? NoiDung { get; set; }     // nội dung đáp án
        public bool IsDung { get; set; }         // đáp án đúng
    }
}