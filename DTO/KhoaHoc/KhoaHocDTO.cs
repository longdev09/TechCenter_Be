using Microsoft.EntityFrameworkCore;

namespace TechCenter.DTO.KhoaHoc
{
    [Keyless]
    public class KhoaHocDTO
    {
        public int IdKhoaHoc { get; set; }
        public string? TenKhoaHoc { get; set; } = null!;
        public string? MoTa { get; set; }
        public decimal? HocPhi { get; set; }
        public string? AnhDaiDien { get; set; }
        public string? TenCapDo { get; set; }
        public string? MauSac { get; set; }
    }
}
