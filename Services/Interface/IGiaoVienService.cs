using TechCenter.DTO.GiaoVien;

namespace TechCenter.Services.Interface
{
    public interface IGiaoVienService
    {

        Task<GiaoVienDTO> CreateHocVien(GiaoVienDTO gv);
    }
}
