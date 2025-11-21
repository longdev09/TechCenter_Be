using System.Collections.Generic;
using System.Threading.Tasks;
using TechCenter.DTO.TaiLieu;

namespace TechCenter.Services.Interface
{
    public interface ITaiLieuService
    {
        Task<object> GetTaiLieuChoHocVienAsync(int idHocVien);
    }
}
