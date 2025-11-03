using TechCenter.DTO.KhoaHoc;

namespace TechCenter.Services.Interface
{
    public interface IKhoaHocService
    {
        Task<List<KhoaHocDTO>> GetAllKhoaHocAsync();
        Task<KhoaHocDTO?> GetKhoaHocById(int id);
    }
}
