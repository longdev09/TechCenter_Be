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

            // Mã đơn hàng (tham khảo, có thể thay cách sinh khác)
            var orderId = DateTime.Now.Ticks.ToString();
            // Lấy IP client
            var ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";

            // Tạo các tham số gửi sang VNPAY
            var vnpParams = new SortedDictionary<string, string>(StringComparer.Ordinal)
                {
                    { "vnp_Version", "2.1.0" },         // API version theo khuyến nghị VNPAY :contentReference[oaicite:0]{index=0}
                    { "vnp_Command", "pay" },
                    { "vnp_TmnCode", vnp_TmnCode },
                    { "vnp_Amount", ((long)(amount * 100)).ToString() }, // nhân ×100 theo hướng dẫn của VNPAY :contentReference[oaicite:1]{index=1}
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

                        // Tạo chuỗi rawData để ký (KHÔNG encode)
                        string rawData = string.Join("&", vnpParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));

                        // Tính secure hash với HMACSHA512
                        string vnp_SecureHash = ComputeHmacSha512(vnp_HashSecret, rawData);

                        // Tạo query string để redirect (encode từng tham số)
                        string queryString = string.Join("&", vnpParams.Select(kvp =>
                            $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"
                        ));

                        // Ghép URL cuối cùng
                        string paymentUrl = $"{vnp_Url}?{queryString}&vnp_SecureHash={vnp_SecureHash}";

                        return Task.FromResult(paymentUrl);
                    }

                    private static string ComputeHmacSha512(string key, string data)
                    {
                        using (var hmac = new System.Security.Cryptography.HMACSHA512(System.Text.Encoding.UTF8.GetBytes(key)))
                        {
                            byte[] hashValue = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data));
                            var sb = new System.Text.StringBuilder();
                            foreach (var b in hashValue)
                            {
                                sb.Append(b.ToString("x2"));
                            }
                            return sb.ToString();
                        }
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
