using Microsoft.EntityFrameworkCore;

namespace TechCenter.DTO.LopHoc
{
 [Keyless]
 public class LopHocForGiaoVienDTO
 {
         public int IdLophoc { get; set; }
         public int IdKhoahoc { get; set; }
         public string? TenKhoaHoc { get; set; }
         public int? IdGvChinh { get; set; }
         public int? IdGiaoVienPhanCong { get; set; }
         public DateTime? NgayKhaiGiang { get; set; }
         public int SiSoHienTai { get; set; }
         public int SiSoToiDa { get; set; }
 }
}