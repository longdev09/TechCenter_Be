using TechCenter.DTO;

namespace TechCenter.Services.Interface
{
    public interface IHocVienService
    {

        Task<HocVienDTO> CreateHocVien(HocVienDTO hocvien);
      
    }
}
