using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCenter.DTO.DangKyHoc;
using TechCenter.Middleware;
using TechCenter.Services.Interface;

namespace TechCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DangKyLopController : ControllerBase
    {
        private readonly IDangKyHocService _dangKyHocService;
        public DangKyLopController(IDangKyHocService dangKyHocService)
        {
            _dangKyHocService = dangKyHocService;
        }
        // POST: api/DangKyLop/Create
        [HttpPost("CreateDangKyLopHocVien")]
        public async Task<IActionResult> CreateDangKyLop([FromBody] InsertDangKyHocDTO dto)
        {
            var dangKyLop = await _dangKyHocService.CreateDangKyLopAsync(dto);
            return Ok(BaseResponse<object>.SuccessCreated());
        }
    }
}
