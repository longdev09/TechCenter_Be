using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCenter.DTO.KhoaHoc;
using TechCenter.Middleware;
using TechCenter.Services.Interface;

namespace TechCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoaHocController : ControllerBase
    {
        private readonly IKhoaHocService _khoaHocService;

        public KhoaHocController(IKhoaHocService khoaHocService)
        {
            _khoaHocService = khoaHocService;
        }

        // GET: api/KhoaHoc
        [HttpGet("getAllKhoaHoc")]
        public async Task<ActionResult<List<KhoaHocDTO>>> GetAll()
        {
            var list = await _khoaHocService.GetAllKhoaHocAsync();
            return Ok(BaseResponse<object>.SuccessFetched(list));
        }

        // GET: api/KhoaHoc/5
        [HttpGet("GetKhoaHocByID")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _khoaHocService.GetKhoaHocById(id);
            return Ok(BaseResponse<object>.SuccessFetched(item));
        }
    }
}
