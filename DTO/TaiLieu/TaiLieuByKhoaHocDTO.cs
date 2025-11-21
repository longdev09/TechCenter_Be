using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TechCenter.DTO.TaiLieu
{
 [Keyless]
 public class TaiLieuByKhoaHocDTO
 {
 public int? IdKhoaHoc { get; set; }
 public string? TenKhoaHoc { get; set; }
 public List<TaiLieuHocVienDTO>? TaiLieu { get; set; }
 }
}