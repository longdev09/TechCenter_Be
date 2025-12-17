using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCenter.DTO.KetQuaThi;
using TechCenter.Middleware;
using TechCenter.Services.Interface;

namespace TechCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KetQuaThiController : ControllerBase
    {
        private readonly IKetQuaThiService _ketQuaThiService;
        public KetQuaThiController(IKetQuaThiService ketQuaThiService)
        {
            _ketQuaThiService = ketQuaThiService;
        }

        [HttpPost("InsertKetQuaThi")]
        public async Task<IActionResult> InsertKetQuaThi([FromBody] InsertKetQuaThiDTO dto)
        {
            var idKetQua = await _ketQuaThiService.InsertKetQuaThiAsync(dto);
            return Ok(BaseResponse<object>.SuccessCreated(idKetQua));
        }

        [HttpPost("InsertChiTietBaiLamTuLuan")]
        public async Task<IActionResult> InsertChiTietBaiLamTuLuan(int idKetQua, [FromBody] List<InsertChiTietTuLuanDTO> dtos)
        {
            await _ketQuaThiService.InsertChiTietBaiLamTuLuanAsync(idKetQua, dtos);
            return Ok(BaseResponse<object>.SuccessCreated(null));
        }
        [HttpPost("InsertChiTietDapAnTracNghiem")]
        public async Task<IActionResult> InsertChiTietDapAnTracNghiem(int idKetQua, [FromBody] List<InsertChiTietTracNghiemDTO> dtos)
        {
            await _ketQuaThiService.InsertChiTietDapAnTracNghiemAsync(idKetQua, dtos);
            return Ok(BaseResponse<object>.SuccessCreated(null));
        }
        [HttpPut("UpdateKetQuaThiAsync")]
        public async Task<IActionResult> UpdateKetQuaThiAsync([FromBody] UpdateKetQuaThiDTO dto)
        {
            var result = await _ketQuaThiService.UpdateKetQuaThiAsync(dto);
            return Ok(BaseResponse<object>.SuccessUpdated(result));
        }

        [HttpGet("GetKetQuaChoGiaoVien")]
        public async Task<IActionResult> GetKetQuaChoGiaoVien(int idGiaoVien)
        {
            var list = await _ketQuaThiService.GetKetQuaChoGiaoVienAsync(idGiaoVien);
            return Ok(BaseResponse<object>.SuccessFetched(list));
        }

        [HttpGet("GetChiTietBaiLamByKetQua")]
        public async Task<IActionResult> GetChiTietBaiLamByKetQua(int idKetQua)
        {
            var data = await _ketQuaThiService.GetChiTietBaiLamByKetQuaAsync(idKetQua);
            return Ok(BaseResponse<object>.SuccessFetched(data));
        }

        [HttpGet("GetKetQuaDaChamByHocVien")]
        public async Task<IActionResult> GetKetQuaDaChamByHocVien(int idHocVien)
        {
            var data = await _ketQuaThiService.GetKetQuaDaChamByHocVienAsync(idHocVien);
            return Ok(BaseResponse<object>.SuccessFetched(data));
        }
    }
}
