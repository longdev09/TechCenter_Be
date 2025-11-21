using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCenter.Middleware;
using TechCenter.Services.Interface;

namespace TechCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiLieuController : ControllerBase
    {
        private readonly ITaiLieuService _taiLieuService;
        public TaiLieuController(ITaiLieuService taiLieuService)
        {
            _taiLieuService = taiLieuService;
        }

        // GET: api/TaiLieu/ChoHocVien/1
        [HttpGet("GetTaiLieuByIdHocVien")]
        public async Task<IActionResult> GetTaiLieuChoHocVien(int idHocVien)
        {
            var list = await _taiLieuService.GetTaiLieuChoHocVienAsync(idHocVien);
            return Ok(BaseResponse<object>.SuccessFetched(list));
        }
    }
}
