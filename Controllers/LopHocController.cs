using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCenter.DTO.LichHoc;
using TechCenter.DTO.LopHoc;
using TechCenter.Middleware;
using TechCenter.Services.Interface;

namespace TechCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LopHocController : ControllerBase
    {
        private readonly ILopHocService _lopHocService;
        public LopHocController(ILopHocService lopHocService)
        {
            _lopHocService = lopHocService;
        }

        // GET api/LopHoc/byKhoaHoc/5
        [HttpGet("GetLopHocByKhoaHoc")]
        public async Task<IActionResult> GetByKhoaHoc(int idKhoaHoc)
        {
            var classes = await _lopHocService.GetLopHocByIdKhoaHoc(idKhoaHoc);
            return Ok(BaseResponse<object>.SuccessFetched(classes));
        }


        // get danh sách lớp theo by id giaoVien

        [HttpGet("GetLopHocByGiaoVien")]
        public async Task<IActionResult> GetLopHocByGiaoVien(int idGiaoVien)
        {
            var classes = await _lopHocService.GetLopHocByGiaoVienAsync(idGiaoVien);
            return Ok(BaseResponse<object>.SuccessFetched(classes));
        }
    }
}
