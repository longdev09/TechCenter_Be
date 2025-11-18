using TechCenter.DTO.HocVien;

namespace TechCenter.Services.Interface
{
    public interface IHocVienService
    {

        Task<HocVienDTO> CreateHocVien(HocVienDTO hocvien);
        Task<HocVienDTO?> GetByIdTaiKhoanAsync(int idTaiKhoan);


    }
}
