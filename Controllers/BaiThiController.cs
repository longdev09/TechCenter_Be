using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCenter.DTO.BaiThi;
using TechCenter.Middleware;
using TechCenter.Services.Interface;

namespace TechCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaiThiController : ControllerBase
    {
        private readonly IBaiThiService baiThiService;
        public BaiThiController(IBaiThiService baiThiService)
        {
            this.baiThiService = baiThiService;
        }

        [HttpGet("GetBaithiByLopId")]
        public async Task<IActionResult> GetBaithiByLopId(int idLop)
        {
            var result = await baiThiService.GetBaithiByLopIdAsync(idLop);
            return Ok(result);
        }

        [HttpPost("CreateBaiThi")]
        public async Task<IActionResult> CreateBaiThi([FromBody] InsertBaiThiDTO dto)
        {
            var newBaiThiId = await baiThiService.InsertBaiThiAsync(dto);
            return Ok(BaseResponse<object>.SuccessCreated(newBaiThiId));
        }
    }
}
