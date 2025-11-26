using Microsoft.EntityFrameworkCore;

namespace TechCenter.DTO.HoaDon
{
 [Keyless]
 public class HoaDonDTO
 {
     public int Idhoadon { get; set; }
     public string? Mahoadon { get; set; }
     public int IdHocvien { get; set; }
     public int IdLophoc { get; set; }
     public DateTime? Ngaytao { get; set; }
     public decimal Tongtien { get; set; }
     public string? Trangthai { get; set; }
     public string? Ghichu { get; set; }
     public string? TenHocVien { get; set; }
     public string? TenKhoaHoc { get; set; }
 }
}
