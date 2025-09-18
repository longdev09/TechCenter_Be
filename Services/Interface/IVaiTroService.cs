using TechCenter.DTO;

namespace TechCenter.Services.Interface
{
    public interface IVaiTroService
    {
        Task<List<VaiTroDTO>> GetAllVaiTro();
        Task<VaiTroDTO?> GetVaiTroById(int id);
        Task<VaiTroDTO> CreateVaiTro(VaiTroDTO vaiTroDto);
        Task<bool> UpdateVaiTro(int id, VaiTroDTO vaiTroDto);
        Task<bool> DeleteVaiTro(int id);
    }
}
