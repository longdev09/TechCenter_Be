using TechCenter.DTO.LichHoc;

namespace TechCenter.Services.Interface
{
    public interface ILichHocService
    {
        Task<List<LichHocChiTietHocVienTuan>> GetLichHocChiTietByIdHocVienTheoTuan(int idHocVien, DateOnly tuNgay, DateOnly denNgay);

    }
}
