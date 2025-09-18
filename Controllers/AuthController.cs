using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCenter.DTO;
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
            if (taoTaiKhoanDTO == null)
                return BadRequest("Dữ liệu tài khoản không hợp lệ.");
            try
            {
                var createdAccount = await _taiKhoanService.CreateTaiKhoan(taoTaiKhoanDTO.Tendangnhap, taoTaiKhoanDTO.Matkhau, taoTaiKhoanDTO.SoDienThoai, taoTaiKhoanDTO.Email, 3);
                // Trả về 201 Created cùng với tài nguyên mới
                return CreatedAtAction(
                    nameof(CreateTaiKhoanHocVien),          
                    createdAccount
                );
            }
            catch (InvalidOperationException ex) // ví dụ email trùng
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Có lỗi xảy ra khi truy xuất dữ liệu {ex.Message}");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] DangNhapDTO dangNhapDTO)
        {
            if (dangNhapDTO == null)
            {
                return BadRequest("Dữ liệu đăng nhập không hợp lệ.");
            }

            try
            {
                var result = await _taiKhoanService.Login(dangNhapDTO);
                return Ok(result);  // Trả về token + user info
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }


    }
}
