using System.Collections.Generic;
using System.Threading.Tasks;
using TechCenter.DTO.BaiThi;

namespace TechCenter.Services.Interface
{
    public interface IBaiThiService
    {
        Task<List<BaithiWithKhoaDTO>> GetBaithiByLopIdAsync(int idLop);

        Task<int> InsertBaiThiAsync(InsertBaiThiDTO dto);

        Task<object> GetBaithiByGiaoVienAsync(int idGiaoVien);
    }
}
