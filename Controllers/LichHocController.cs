using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCenter.DTO.LichHoc;
using TechCenter.Middleware;
using TechCenter.Services.Interface;

namespace TechCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichHocController : ControllerBase
    {
        private readonly ILichHocService _lichHocService;

        public LichHocController(ILichHocService lichHocService)
        {
            _lichHocService = lichHocService;
        }



        // GET: api/LichHoc/ByHocVienTheoTuan?idHocVien=1&tuNgay=2025-01-01&denNgay=2025-01-07
        [HttpGet("GetLichHocChiTietByHocVienTheoTuan")]
        public async Task<IActionResult> GetLichHocChiTietByHocVienTheoTuan([FromQuery] int idHocVien, [FromQuery] string tuNgay, [FromQuery] string denNgay)
        {
            if (idHocVien <= 0)
                return BadRequest(BaseResponse<object>.Fail("idHocVien không hợp lệ", 400));

            if (!DateOnly.TryParse(tuNgay, out var tu))
                return BadRequest(BaseResponse<object>.Fail("tuNgay không hợp lệ. Định dạng yyyy-MM-dd", 400));

            if (!DateOnly.TryParse(denNgay, out var den))
                return BadRequest(BaseResponse<object>.Fail("denNgay không hợp lệ. Định dạng yyyy-MM-dd", 400));

            var list = await _lichHocService.GetLichHocChiTietByIdHocVienTheoTuan(idHocVien, tu, den);
            return Ok(BaseResponse<object>.SuccessFetched(list));
        }

        // GET: api/LichHoc/GiaoVienTuan?idGiaoVien=1&tuNgay=2025-05-12&denNgay=2025-05-18
        [HttpGet("GiaoVienTuan")]
        public async Task<IActionResult> GetLichGiaoVienTheoTuan(int idGiaoVien, DateOnly tuNgay, DateOnly denNgay)
        {
            var data = await _lichHocService.GetLichHocChiTietByIdGiaoVienTheoTuan(idGiaoVien, tuNgay, denNgay);
            return Ok(BaseResponse<object>.SuccessFetched(data));
        }
    }
}
