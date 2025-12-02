using TechCenter.DTO.CauHoi;

namespace TechCenter.Services.Interface
{
    public interface ICauHoiService
    {
        Task<LoaiCauHoiDTO> GetLoaiCauHoiByIdAsync(int id);
        Task<List<LoaiCauHoiDTO>> GetAllLoaiCauHoiAsync();
        Task<InsertCauHoiDTO> InsertCauHoiAsync(InsertCauHoiDTO dto);   
        Task<UpdateCauHoiDTO> UpdateCauHoiAsync(UpdateCauHoiDTO dto);
        Task<InsertDapAnDTO> InsertDapAnAsync(InsertDapAnDTO dto);
        Task<List<InsertDapAnDTO>> InsertDapAnAsync(List<InsertDapAnDTO> dtos);
        Task<List<CauHoiByIdByBaiThiDTO>> GetCauHoiByBaiThiAsync(int idBaiThi);
        Task<InsertCauHoiCodeDTO> InsertCauHoiCodeAsync(InsertCauHoiCodeDTO dto);

    }
}
