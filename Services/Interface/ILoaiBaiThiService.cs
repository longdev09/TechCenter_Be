using TechCenter.DTO.LoaiBaiThi;
using TechCenter.Models;

namespace TechCenter.Services.Interface
{
    public interface ILoaiBaiThiService
    {
        Task<List<LoaiBaiThiDTO>> GetAllLoaiBaiThiAsync();
    }
}
