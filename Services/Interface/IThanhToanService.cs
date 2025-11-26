using System.Collections.Generic;
using System.Threading.Tasks;
using TechCenter.Models;
using TechCenter.DTO.ThanhToan;

namespace TechCenter.Services.Interface
{
    public interface IThanhToanService
    {

        Task<string> CreateThanhToanKhoaHocAsync(long tienThanhToan, int idHv);
    }
}
