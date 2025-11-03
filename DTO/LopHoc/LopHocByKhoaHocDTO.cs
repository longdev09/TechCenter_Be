using Microsoft.EntityFrameworkCore;
using TechCenter.DTO.LichHoc;
using System.Collections.Generic;

namespace TechCenter.DTO.LopHoc
{
    [Keyless]
    public class LopHocByKhoaHocDTO
    {
        public DateTime? NgayKhaiGiang { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? SiSoHienTai { get; set; }
        public int? SiSoToiDa { get; set; }
        public int? IdKhoaHoc { get; set; }
        public string? HoTenGV { get; set; }

        // Thêm danh sách lịch học của lớp
        public List<LichHocDTO>? LichHocs { get; set; }
    }
}
