using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCenter.Middleware;
using TechCenter.Services.Interface;

namespace TechCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThanhToanPayController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;

        public ThanhToanPayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        // POST: api/ThanhToanPay/create
        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment([FromBody] decimal amount)
        {
            try
            {
                if (amount <= 0)
                    return BadRequest(BaseResponse<object>.Fail("Số tiền không hợp lệ", 400));

                var url = await _vnPayService.CreatePaymentUrlAsync(amount);
                return Ok(BaseResponse<object>.Success(new { paymentUrl = url }, "Tạo url thanh toán thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, BaseResponse<object>.Fail(ex.Message));
            }
        }

        // GET: api/ThanhToanPay/return
        [HttpGet("ReturnPayment")]
        public async Task<IActionResult> VNPayReturn()
        {
            try
            {
                var result = await _vnPayService.VNPayReturnAsync(Request.Query);
                if (result.IsSuccess)
                    return Ok(BaseResponse<object>.Success(result.Message));

                return BadRequest(BaseResponse<object>.Fail(result.Message, 400));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, BaseResponse<object>.Fail(ex.Message));
            }
        }
    }
}
