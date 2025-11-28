using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCenter.Middleware;
using TechCenter.Services.Interface;

namespace TechCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiBaiThiController : ControllerBase
    {
        private readonly ILoaiBaiThiService _loaiBaiThiService;
        public LoaiBaiThiController(ILoaiBaiThiService loaiBaiThiService)
        {
            _loaiBaiThiService = loaiBaiThiService;
        }

        [HttpGet("GetAllLoaiBaiThi")]
        public async Task<IActionResult> GetAllLoaiBaiThi()
        {
            var result = await _loaiBaiThiService.GetAllLoaiBaiThiAsync();
            return Ok(BaseResponse<object>.SuccessFetched(result));
        }
    }
}
