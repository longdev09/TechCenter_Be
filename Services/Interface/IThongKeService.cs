using System.Threading.Tasks;
using TechCenter.DTO.ThongKe;

namespace TechCenter.Services.Interface
{
    public interface IThongKeService
    {
        Task<ThongKeHocVienDTO> GetThongKeHocVienAsync(int idHocVien);

        // NEW: thống kê cho GIÁO VIÊN
        Task<ThongKeGiaoVienDTO> GetThongKeGiaoVienAsync(int idGiaoVien);
    }
}
