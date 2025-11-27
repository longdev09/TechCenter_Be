using Microsoft.EntityFrameworkCore;

namespace TechCenter.DTO.GiaoVien
{
 [Keyless]
 public class GiaoVienDTO
 {
         public int? idTaiKhoan { get; set; }
         public int? idGiaoVien { get; set; }
         public string? hoTenGv { get; set; }
         public string? gioiTinhGv { get; set; }
         public DateOnly? ngaySinhGv { get; set; }
         public string? diaChiGv { get; set; }
         public string? anhGv { get; set; }
         public string? email { get; set; }
 }
}
