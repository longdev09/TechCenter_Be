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

        public ThanhToanPayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
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



        [HttpPost]
        [Route("tao-thanh-toan")]
        public IActionResult taoThanhToan(long tongtien, string maHv)
        {
            string vnp_Returnurl = "https://localhost:44352/api/HoaDon/tao-hoa-don-mua-khoa-hoc";
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            string vnp_TmnCode = "M09BPYD2";
            string vnp_HashSecret = "W8WYMTMFQAK29TY6GLWCV3LSJGBG2OB1";
            //if (string.IsNullOrEmpty(vnp_TmnCode) || string.IsNullOrEmpty(vnp_HashSecret))
            //{
            //    _logger.LogError("Vui lòng cấu hình các tham số: vnp_TmnCode, vnp_HashSecret trong file appsettings.json");
            //    return BadRequest("Vui lòng cấu hình các tham số: vnp_TmnCode, vnp_HashSecret trong file appsettings.json");
            //}

            OrderInfo order = new OrderInfo();
            order.OrderId = DateTime.Now.Ticks; // Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
            order.Amount = tongtien; // Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY 100,000 VND
            order.Status = "0"; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending" khởi tạo giao dịch chưa có IPN
            order.CreatedDate = DateTime.Now;


            VnPayLibrary vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000

            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", "192.168.56.1");
            vnpay.AddRequestData("vnp_Locale", "vn");

            vnpay.AddRequestData("vnp_OrderInfo", maHv + "," + tongtien);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy 
            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            //log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            return Ok(new { RedirectUrl = paymentUrl });

        }
    }
}
