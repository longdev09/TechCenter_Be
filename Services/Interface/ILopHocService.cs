using TechCenter.DTO.LichHoc;
using TechCenter.DTO.LopHoc;

namespace TechCenter.Services.Interface
{
    public interface ILopHocService
    {
        Task<List<LopHocByKhoaHocDTO>> GetLopHocByIdKhoaHoc(int idKhoaHoc);
    }
}
