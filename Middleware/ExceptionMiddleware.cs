using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

namespace TechCenter.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Đã xảy ra lỗi trong quá trình xử lý request");

                await HandleExceptionAsync(context, ex);
            }
        }


        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            BaseResponse<string> response;

            if (_env != null && _env.IsDevelopment())
            {
                // In development include full exception details
                response = new BaseResponse<string>(context.Response.StatusCode, exception.Message, exception.ToString());
            }
            else
            {
                // Production: return only message
                response = BaseResponse<string>.Fail(
                    message: exception.Message,
                    status: context.Response.StatusCode
                );
            }

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }

    }
}
