using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.CauHoi;
using System;
using System.Collections.Generic;

namespace TechCenter.DTO.BaiThi
{
    [Keyless]
    public class BaithiWithCauHoiDTO
    {
        public int IdBaithi { get; set; }
        public string? Tieude { get; set; }
        public int? Thoiluong { get; set; }
        public DateTime? Ngaytao { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? Mota { get; set; }
        public int? IdLop { get; set; }
        public int? IdLoaibaithi { get; set; }

        public List<CauHoiWithDapAnDTO> CauHois { get; set; } = new();
    }
}