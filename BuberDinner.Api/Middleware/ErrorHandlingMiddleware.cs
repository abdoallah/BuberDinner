using System.Net;
using System.Text.Json;

namespace BuberDinner.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context , e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new { error = "An error occured while proccessing" });
            context.Response.ContentType= "application/json";
            context.Response.StatusCode = (int)code;   
            return context.Response.WriteAsync(result);
        }
    }
}
