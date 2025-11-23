using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCenter.DTO.HocVien;
using TechCenter.Middleware;
using TechCenter.Services.Interface;

namespace TechCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocVienController : ControllerBase
    {
        private readonly IHocVienService _hocVienService;

        public HocVienController(IHocVienService hocVienService)
        {
            _hocVienService = hocVienService;
        }

        // POST: api/HocVien
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HocVienDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(BaseResponse<object>.Fail("Dữ liệu không hợp lệ", 400));

                var created = await _hocVienService.CreateHocVien(dto);
                return Ok(BaseResponse<object>.Success(created, "Tạo học viên thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, BaseResponse<object>.Fail(ex.Message));
            }
        }

        // GET: api/HocVien/byTaiKhoan/5
        [HttpGet("GetHocVienByIdTaiKhoan")]
        public async Task<IActionResult> GetByTaiKhoan(int idTaiKhoan)
        {
            var hv = await _hocVienService.GetByIdTaiKhoanAsync(idTaiKhoan);
            return Ok(BaseResponse<object>.SuccessFetched(hv));
        }

        [HttpGet("GetHocVienById")]
        public async Task<IActionResult> GetById(int idHocVien)
        {
            var hv = await _hocVienService.GetHocVienByIdAsync(idHocVien);
            return Ok(BaseResponse<object>.SuccessFetched(hv));
        }


        // Accepts multipart/form-data if including file (IFormFile in UpdateHocVienDTO)
        [HttpPut("UpdateHocVien")]
        public async Task<IActionResult> UpdateHocVien([FromForm] UpdateHocVienDTO dto)
        {
            var updated = await _hocVienService.UpdateHocVienAsync(dto);
            if (updated == null)
                return NotFound(BaseResponse<object>.Fail("Học viên không tồn tại", 404));

            return Ok(BaseResponse<object>.SuccessUpdated(updated));
        }
    }
}
