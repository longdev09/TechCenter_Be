using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCenter.DTO;
using TechCenter.Middleware;
using TechCenter.Services.Interface;

namespace TechCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ITaiKhoanService _taiKhoanService;
        public AuthController(ITaiKhoanService taiKhoanService)
        {
            _taiKhoanService = taiKhoanService;
        }

        [HttpPost("CreateTaiKhoanHocVien")]
        public async Task<IActionResult> CreateTaiKhoanHocVien([FromBody] TaoTaiKhoanDTO taoTaiKhoanDTO)
        {
           
            var createdAccount = await _taiKhoanService.CreateTaiKhoan(taoTaiKhoanDTO.Tendangnhap, taoTaiKhoanDTO.Matkhau, taoTaiKhoanDTO.Email, 2, taoTaiKhoanDTO.Hoten);
            return Ok(BaseResponse<object>.SuccessCreated(createdAccount));

        }


        [HttpPost("CreateTaiKhoanGiaoVien")]
        public async Task<IActionResult> CreateTaiKhoanGiaoVien([FromBody] TaoTaiKhoanDTO taoTaiKhoanDTO)
        {

            var createdAccount = await _taiKhoanService.CreateTaiKhoan(taoTaiKhoanDTO.Tendangnhap, taoTaiKhoanDTO.Matkhau, taoTaiKhoanDTO.Email, 1, taoTaiKhoanDTO.Hoten);
            return Ok(BaseResponse<object>.SuccessCreated(createdAccount));

        }





        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] DangNhapDTO dangNhapDTO)
        {
          

            var result = await _taiKhoanService.Login(dangNhapDTO);
            return Ok(BaseResponse<object>.SuccessFetched(result));
        }


        [HttpGet("GetInfoNguoiDung")]
        public async Task<IActionResult> GetInfoNguoiDung(int? idTaiKhoan,  int? vt)
        {
            var result = await _taiKhoanService.GetInfoNguoiDung(idTaiKhoan, vt);
            return Ok(BaseResponse<object>.SuccessFetched(result));
        }


    }
}
