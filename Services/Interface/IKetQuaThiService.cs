using TechCenter.DTO.KetQuaThi;

namespace TechCenter.Services.Interface
{
    public interface IKetQuaThiService
    {
        Task<int> InsertKetQuaThiAsync(InsertKetQuaThiDTO dto);
        Task InsertChiTietBaiLamTuLuanAsync(
           int idKetQua,
           List<InsertChiTietTuLuanDTO> dtos);
        Task<int> InsertChiTietDapAnTracNghiemAsync(
            int idKetQua,
            List<InsertChiTietTracNghiemDTO> dtos);

        Task<bool> UpdateKetQuaThiAsync(UpdateKetQuaThiDTO dto);

        Task<List<KetQuaForGiaoVienDTO>> GetKetQuaChoGiaoVienAsync(int idGiaoVien);

 
        Task<List<ChiTietCauHoiBaiLamDTO>> GetChiTietBaiLamByKetQuaAsync(int idKetQua);
        Task<List<KetQuaHocVienByMonDTO>> GetKetQuaDaChamByHocVienAsync(int idHocVien);
    }
}
