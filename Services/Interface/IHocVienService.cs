using TechCenter.DTO.HocVien;

namespace TechCenter.Services.Interface
{
    public interface IHocVienService
    {

        Task<HocVienDTO> CreateHocVien(HocVienDTO hocvien);
        Task<HocVienDTO?> GetByIdTaiKhoanAsync(int idTaiKhoan);

        Task<HocVienByIdDTO?> GetHocVienByIdAsync(int idHocVien);

        Task<HocVienByIdDTO?> UpdateHocVienAsync(UpdateHocVienDTO dto);


    }
}
