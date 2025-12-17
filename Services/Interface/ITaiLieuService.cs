using System.Collections.Generic;
using System.Threading.Tasks;
using TechCenter.DTO.TaiLieu;
using TechCenter.Models;

namespace TechCenter.Services.Interface
{
    public interface ITaiLieuService
    {

        Task ThemTaiLieuAsync(Tailieu tailieu, IFormFile? file, int? idLophoc = null, string folder = "tailieu");
        Task<object> GetTaiLieuChoHocVienAsync(int idHocVien);
        Task<List<TaiLieuByLopDTO>> GetTaiLieuByLopAsync(int idLophoc);
    }
}
