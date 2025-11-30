using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCenter.DTO.CauHoi;
using TechCenter.Middleware;
using TechCenter.Services.Interface;

namespace TechCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CauHoiController : ControllerBase
    {
        private readonly ICauHoiService cauHoiService;
        public CauHoiController(ICauHoiService cauHoiService)
        {
            this.cauHoiService = cauHoiService;
        }


        [HttpGet("GetAllLoaiCauHoi")]
        public async Task<IActionResult> GetAllLoaiCauHoiDto()
        {
            var result = await cauHoiService.GetAllLoaiCauHoiAsync();
            return Ok(BaseResponse<object>.SuccessFetched(result));
        }

        [HttpGet("GetLoaiCauHoiById/{id}")]
        public async Task<IActionResult> GetLoaiCauHoiById(int id)
        {
            var result = await cauHoiService.GetLoaiCauHoiByIdAsync(id);
            if (result == null)
                return NotFound(BaseResponse<object>.Fail("Loai cau hoi not found"));

            return Ok(BaseResponse<object>.SuccessFetched(result));
        }

        [HttpPost("InsertCauHoi")]
        public async Task<IActionResult> InsertCauHoi([FromBody] InsertCauHoiDTO dto)
        {
            var result = await cauHoiService.InsertCauHoiAsync(dto);
            return Ok(BaseResponse<object>.SuccessCreated(result));
        } 

        [HttpPut("UpdateCauHoi")]
        public async Task<IActionResult> UpdateCauHoi([FromBody] UpdateCauHoiDTO dto)
        {
            var result = await cauHoiService.UpdateCauHoiAsync(dto);
            if (result == null)
                return NotFound(BaseResponse<object>.Fail("Cau hoi not found or invalid request"));

            return Ok(BaseResponse<object>.SuccessUpdated(result));
        }


        [HttpPost("InsertDapAn")]
        public async Task<IActionResult> InsertDapAn([FromBody] List<InsertDapAnDTO> dtos)
        {
            var result = await cauHoiService.InsertDapAnAsync(dtos);
            return Ok(BaseResponse<object>.SuccessCreated(result));
        }
    }
}
