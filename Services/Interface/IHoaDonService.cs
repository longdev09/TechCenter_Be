using System.Collections.Generic;
using System.Threading.Tasks;
using TechCenter.DTO.HoaDon;

namespace TechCenter.Services.Interface
{
 public interface IHoaDonService
 {
        Task<HoaDonDTO> CreateAsync(InsertHoaDonDTO dto);
 }
}
