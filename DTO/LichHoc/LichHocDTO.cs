using Microsoft.EntityFrameworkCore;

namespace TechCenter.DTO.LichHoc
{
 [Keyless]
 public class LichHocDTO
 {
 public int IdLichhoc { get; set; }
 public int IdLophoc { get; set; }
 public int Thu { get; set; }
 public DateTime Giobatdau { get; set; }
 public DateTime Gioketthuc { get; set; }
 public string? ThuName { get; set; }
 }
}
