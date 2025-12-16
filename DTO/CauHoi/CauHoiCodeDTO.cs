using Microsoft.EntityFrameworkCore;

namespace TechCenter.DTO.CauHoi
{
    [Keyless]
    public class CauHoiCodeDTO
    {
        public int IdCauHoi { get; set; }
        public string? CodeMau { get; set; }
        public string? NgonNgu { get; set; }
    }
}