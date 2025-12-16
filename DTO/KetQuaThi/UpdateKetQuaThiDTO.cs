using Microsoft.EntityFrameworkCore;

namespace TechCenter.DTO.KetQuaThi
{
    public class UpdateKetQuaThiDTO
    {
        public int IdKetqua { get; set; }
        // optional updates
        public DateTime? ThoiGianKetThuc { get; set; }
        public double? TongDiem { get; set; }
        public string? TrangThai { get; set; }
        public string? TrangThaiCham { get; set; }
    }
}
