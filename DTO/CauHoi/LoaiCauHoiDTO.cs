using Microsoft.EntityFrameworkCore;

namespace TechCenter.DTO.CauHoi
{
    [Keyless]
    public class LoaiCauHoiDTO
    {
        public int? Id_LoaiCauHoi { get; set; }
        public string? TenLoaiCauHoi { get; set; }
        public string? Mota { get; set; }
    }
}
