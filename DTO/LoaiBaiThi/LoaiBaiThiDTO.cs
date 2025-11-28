using Microsoft.EntityFrameworkCore;

namespace TechCenter.DTO.LoaiBaiThi
{
    [Keyless]
    public class LoaiBaiThiDTO
    {
        public int IdLoaibaithi { get; set; }
        public string? Tenloai { get; set; }
    }
}
