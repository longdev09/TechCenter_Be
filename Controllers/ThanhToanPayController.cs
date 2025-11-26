using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server_WebBanKhoaHoc.ModelsVnPay;
using TechCenter.Middleware;
using TechCenter.Services.Interface;

namespace TechCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThanhToanPayController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IThanhToanService _thanhToanService;
        public ThanhToanPayController(IVnPayService vnPayService, IThanhToanService thanhToanService)
        {
            _vnPayService = vnPayService;
            _thanhToanService = thanhToanService;
        }

        // POST: api/ThanhToanPay/create
        [HttpGet("CreatePayment")]
        public async Task<IActionResult> CreatePayment( decimal amount)
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


        public class ThanhToanRequest
        {
            public long tongtien { get; set; }
            public int idHv { get; set; }
        }


        [HttpPost]
        [Route("TaoThanhToanVnPay")]
        public IActionResult thanhToan(ThanhToanRequest dto)
        {
            var result = _thanhToanService.CreateThanhToanKhoaHocAsync(dto.tongtien, dto.idHv);
            return Ok(BaseResponse<object>.Success(new { paymentUrl = result }, "Tạo url thanh toán thành công"));
        }
    }
}
