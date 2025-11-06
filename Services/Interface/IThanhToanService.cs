using System.Collections.Generic;
using System.Threading.Tasks;
using TechCenter.Models;
using TechCenter.DTO.ThanhToan;

namespace TechCenter.Services.Interface
{
    public interface IThanhToanService
    {
        Task<List<Thanhtoan>> GetAllAsync();
        Task<Thanhtoan?> GetByIdAsync(int id);
        Task<Thanhtoan> CreateAsync(InsertThanhToanDTO thanhtoan);
        Task<bool> UpdateAsync(Thanhtoan thanhtoan);
        Task<bool> DeleteAsync(int id);
    }
}
