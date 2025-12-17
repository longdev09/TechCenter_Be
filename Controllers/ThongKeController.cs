using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCenter.Middleware;
using TechCenter.Services.Interface;

namespace TechCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly IThongKeService _thongKeService;
        public ThongKeController(IThongKeService thongKeService)
        {
            _thongKeService = thongKeService;
        }

        // Thống kê cho HỌC VIÊN (nếu đã có)
        [HttpGet("ThongKeHocVien")]
        public async Task<IActionResult> GetThongKeHocVien(int idHocVien)
        {
            var data = await _thongKeService.GetThongKeHocVienAsync(idHocVien);
            return Ok(BaseResponse<object>.SuccessFetched(data));
        }

        // NEW: Thống kê cho GIẢNG VIÊN
        // GET: api/ThongKe/GiaoVien?idGiaoVien=1
        [HttpGet("ThongKeGiaoVien")]
        public async Task<IActionResult> GetThongKeGiaoVien(int idGiaoVien)
        {
            var data = await _thongKeService.GetThongKeGiaoVienAsync(idGiaoVien);
            return Ok(BaseResponse<object>.SuccessFetched(data));
        }
    }
}
