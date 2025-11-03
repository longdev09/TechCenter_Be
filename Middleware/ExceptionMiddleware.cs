using System.Net;
using System.Text.Json;

namespace TechCenter.Middleware
{
    namespace TechCenter.Middleware
    {
        public class ExceptionMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILogger<ExceptionMiddleware> _logger;
            public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
            {
                _next = next;
                _logger = logger;
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


            private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = BaseResponse<string>.Fail(
                    message: exception.Message,
                    status: context.Response.StatusCode
                );

                var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                await context.Response.WriteAsync(json);
            }

        }
    }

}
