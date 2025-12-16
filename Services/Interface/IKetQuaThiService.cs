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
    }
}
