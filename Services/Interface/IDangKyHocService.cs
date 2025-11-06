using TechCenter.DTO.DangKyHoc;
using TechCenter.Models;

namespace TechCenter.Services.Interface
{
    public interface IDangKyHocService
    {

        Task<Dangkylop> CreateDangKyLopAsync(InsertDangKyHocDTO dto);
    }
}
