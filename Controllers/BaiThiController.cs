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

        [HttpGet("GetBaithiByGiaoVien")]
        public async Task<IActionResult> GetBaithiByGiaoVien(int idGiaoVien)
        {
            var result = await baiThiService.GetBaithiByGiaoVienAsync(idGiaoVien);
            return Ok(BaseResponse<object>.SuccessFetched(result));
        }


        [HttpGet("GetLoaiBaiThiByLopId")]
        public async Task<IActionResult> GetLoaiBaiThiByLopId(int idLop)
        {
            var result = await baiThiService.GetLoaiBaiThiByLopIdAsync(idLop);
            return Ok(BaseResponse<object>.SuccessFetched(result));
        }
        [HttpGet("GetCauHoiFullByBaiThi")]
        public async Task<IActionResult> GetCauHoiFullByBaiThi(int idBaiThi)
        {
            var result = await baiThiService.GetCauHoiFullByBaiThiAsync(idBaiThi);
            return Ok(BaseResponse<object>.SuccessFetched(result));
        }

        [HttpGet("GetInfoLopByIdBaiThi")]
        public async Task<IActionResult> GetInfoLopByIdBaiThi(int idBaithi)
        {
            var result = await baiThiService.GetBaithiByIdAsync(idBaithi);
            return Ok(BaseResponse<object>.SuccessFetched(result));
        }
    }
}
