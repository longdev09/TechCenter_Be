using Microsoft.EntityFrameworkCore;

namespace TechCenter.DTO.LopHoc
{
    [Keyless]
    public class LopHocByHocVienDTO
    {

 
            public int IdLophoc { get; set; }
            public int IdKhoahoc { get; set; }
            public string? TenKhoaHoc { get; set; }
            public DateTime? NgayKhaiGiang { get; set; }
            public int SiSoHienTai { get; set; }
            public int SiSoToiDa { get; set; }
            public int IdDangKy { get; set; }
            public DateTime NgayDangKy { get; set; }
            public List<TechCenter.DTO.LichHoc.LichHocDTO>? LichHocs { get; set; }
            public string? TenGiaoVien { get; set; }
            public string? AnhGiaoVien { get; set; }

    }
}
