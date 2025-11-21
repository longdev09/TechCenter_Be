using Microsoft.EntityFrameworkCore;

namespace TechCenter.DTO.TaiLieu
{
 [Keyless]
 public class TaiLieuHocVienDTO
 {
     public int IdTailieu { get; set; }
     public string? Tieudetl { get; set; }
     public string? Motatl { get; set; }
     public string? Urltailieu { get; set; }
     public DateTime Ngaydangtl { get; set; }
     public bool IsPublic { get; set; }

     public int? IdLophoc { get; set; }
     public string? TenKhoaHoc { get; set; }
     public string? Hotengv { get; set; }
 }
}
