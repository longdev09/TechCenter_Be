using Microsoft.EntityFrameworkCore;

namespace TechCenter.DTO.CauHoi
{
    [Keyless]
    public class DapAnDTO
    {
        public int IdDapAn { get; set; }
        public int IdCauHoi { get; set; }
        public string Ma { get; set; } = string.Empty;
        public bool IsDung { get; set; }
    }
}