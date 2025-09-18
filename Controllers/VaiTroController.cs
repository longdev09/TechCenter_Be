using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCenter.DTO;
using TechCenter.Services.Interface;

namespace TechCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaiTroController : ControllerBase
    {
        private readonly IVaiTroService _vaiTroService;
        public VaiTroController(IVaiTroService vaiTroService)
        {
            _vaiTroService = vaiTroService;
        }

        [HttpGet("GetAllVaiTro")]
        public async Task<IActionResult> GetAllVaiTro()
        {
           try
           {
               var vt =  await _vaiTroService.GetAllVaiTro();
                return Ok(vt);
           }
           catch (Exception ex)
           {
                return StatusCode(500, $"Có lỗi xảy ra khi truy xuất dữ liệu {ex.Message}");
           }
        }
        
        [HttpGet("GetVaiTroById")]
        public async Task<IActionResult> GetVaiTroById(int id)
        {
            try
            {
                var vt = await _vaiTroService.GetVaiTroById(id);
                if (vt == null)
                {
                    return NotFound();
                }
                return Ok(vt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Có lỗi xảy ra khi truy xuất dữ liệu {ex.Message}");
            }
        }
        [HttpPost("CreateVaiTro")]
        public async Task<IActionResult> CreateVaiTro([FromBody] VaiTroDTO vaiTroDto)
        {
            if (vaiTroDto == null)
                return BadRequest("Dữ liệu vai trò không hợp lệ.");
            try
            {
                var createdVaiTro = await _vaiTroService.CreateVaiTro(vaiTroDto);
                return CreatedAtAction(
                    nameof(CreateVaiTro),
                    new { id = createdVaiTro.Id_VaiTro },
                    createdVaiTro
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Có lỗi xảy ra khi truy xuất dữ liệu {ex.Message}");
            }
        }
        [HttpPut("UpdateVaiTro")]
        public async Task<IActionResult> UpdateVaiTro(int id, [FromBody] VaiTroDTO vaiTroDto)
        {
            if (vaiTroDto == null || id != vaiTroDto.Id_VaiTro)
                return BadRequest("Dữ liệu vai trò không hợp lệ.");
            try
            {
                var result = await _vaiTroService.UpdateVaiTro(id, vaiTroDto);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Có lỗi xảy ra khi truy xuất dữ liệu {ex.Message}");
            }
        }
        [HttpDelete("DeleteVaiTro")]

        public async Task<IActionResult> DeleteVaiTro(int id)
        {
            try
            {
                var result = await _vaiTroService.DeleteVaiTro(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Có lỗi xảy ra khi truy xuất dữ liệu {ex.Message}");
            }
        }   
    }
}
