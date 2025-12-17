using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCenter.Middleware;
using TechCenter.Models;
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
        [HttpPost("InsertTaiLieu")]
        public async Task<IActionResult> InsertTaiLieu(
            [FromForm] Tailieu tailieu,
                 int? idLophoc,
             IFormFile? file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _taiLieuService.ThemTaiLieuAsync(tailieu, file, idLophoc);

            return Ok(BaseResponse<object>.SuccessCreated(null));
        }

        [HttpGet("GetTaiLieuByLop")]
        public async Task<IActionResult> GetTaiLieuByLop(int idLophoc)
        {
            var list = await _taiLieuService.GetTaiLieuByLopAsync(idLophoc);
            return Ok(BaseResponse<object>.SuccessFetched(list));
        }
    }
}
