using System;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.DTO.BaiThi
{
    [Keyless]
    public class BaiThiLopHocDTO
    {
        public int IdBaithi { get; set; }

        // class / course
        public int? IdLop { get; set; }
        public int? IdKhoahoc { get; set; }
        public string? TenKhoaHoc { get; set; }

        // baithi info
        public string? Tieude { get; set; }
        public int? Thoiluong { get; set; }
        public DateTime? Ngaytao { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        // student-specific status (computed from Ketquathi)
        public string? TrangThaiKetQua { get; set; }    // e.g. "DANG_LAM", "DA_NOP"
        public bool DaThi { get; set; }                 // true if student has any ketqua
        public bool ChoPhepThi { get; set; }            // derived rule: allowed to take

        // optional: extra metadata
        public string? Mota { get; set; }
    }
}
