using TechCenter.DTO.LichHoc;

namespace TechCenter.Services.Interface
{
    public interface ILichHocService
    {
        Task<List<LichHocChiTietHocVienTuan>> GetLichHocChiTietByIdHocVienTheoTuan(int idHocVien, DateOnly tuNgay, DateOnly denNgay);

        // MỚI: lịch chi tiết theo tuần cho GIẢNG VIÊN
        Task<List<LichHocChiTietGiaoVienTuan>> GetLichHocChiTietByIdGiaoVienTheoTuan(int idGiaoVien, DateOnly tuNgay, DateOnly denNgay);
    }
}
