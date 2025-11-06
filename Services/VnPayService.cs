using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using TechCenter.Services.Interface;
using static System.Net.WebRequestMethods;

namespace TechCenter.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;


    public VnPayService(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }


        // tạo thanh toán
        public Task<string> CreatePaymentUrlAsync(decimal amount)
        {
            var vnp_TmnCode = _config["VNPay:TmnCode"];
            var vnp_HashSecret = _config["VNPay:HashSecret"];
            var vnp_Url = _config["VNPay:BaseUrl"];
            var vnp_ReturnUrl = _config["VNPay:ReturnUrl"];

            var orderId = DateTime.Now.Ticks.ToString();
            var ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";

            var vnpParams = new SortedDictionary<string, string>(StringComparer.Ordinal)
            {
                { "vnp_Version", "2.1.1" }, 
                { "vnp_Command", "pay" },
                { "vnp_TmnCode", vnp_TmnCode },
                { "vnp_Amount", ((long)(amount * 100)).ToString() },
                { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") },
                { "vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss") }, 
                { "vnp_CurrCode", "VND" },
                { "vnp_IpAddr", ip },
                { "vnp_Locale", "vn" },
                { "vnp_OrderInfo", $"Thanh toan don hang thoi gian: {DateTime.Now:yyyy-MM-dd HH:mm:ss}" },
                { "vnp_OrderType", "topup" },
                { "vnp_ReturnUrl", vnp_ReturnUrl },
                { "vnp_TxnRef", orderId }
            };

            // 🔹 Tạo chuỗi rawData để ký
            var rawData = string.Join("&", vnpParams.Select(x => $"{x.Key}={x.Value}"));
            var secureHash = HmacSHA512(vnp_HashSecret, rawData);

            // 🔹 Encode và ghép URL thanh toán
            var query = string.Join("&", vnpParams.Select(x => $"{x.Key}={HttpUtility.UrlEncode(x.Value)}"));
            var paymentUrl = $"{vnp_Url}?{query}&vnp_SecureHash={secureHash}";

            return Task.FromResult(paymentUrl);
        }


        public static string HmacSHA512(string key, string input)
        {
            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
            byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hashValue).Replace("-", "").ToLower();
        }
        public async Task<(bool IsSuccess, string Message)> VNPayReturnAsync(IQueryCollection query)
        {
            var vnp_HashSecret = _config["VNPay:HashSecret"];

            var vnpData = new Dictionary<string, string>();
            foreach (var key in query.Keys)
            {
                if (key != "vnp_SecureHash" && key != "vnp_SecureHashType")
                    vnpData.Add(key, query[key]!);
            }

            var rawData = string.Join("&", vnpData.OrderBy(p => p.Key)
                .Select(p => $"{p.Key}={p.Value}"));
            var secureHash = HmacSHA512(vnp_HashSecret, rawData);

            var vnp_SecureHash = query["vnp_SecureHash"].ToString();

            if (secureHash.Equals(vnp_SecureHash, StringComparison.OrdinalIgnoreCase))
            {
                var responseCode = query["vnp_ResponseCode"].ToString();
                if (responseCode == "00")
                    return (true, "Thanh toán thành công");
                else
                    return (false, $"Thanh toán thất bại. Mã lỗi: {responseCode}");
            }

            return (false, "Chữ ký không hợp lệ");
        }
    }

}
