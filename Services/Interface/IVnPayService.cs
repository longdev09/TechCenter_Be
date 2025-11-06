using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TechCenter.Services.Interface
{
     public interface IVnPayService
     {
          Task<string> CreatePaymentUrlAsync(decimal amount);
          Task<(bool IsSuccess, string Message)> VNPayReturnAsync(IQueryCollection query);
      }
}
