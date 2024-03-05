using Awushi.Application.Exceptions;
using Awushi.Web.Models;
using System.Net;

namespace Awushi.Web.Middldwares
{
    public class ExcepetionMiddleware
    {
        public readonly RequestDelegate _next;
        public ExcepetionMiddleware(RequestDelegate next)
        {
            _next = next;       
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);      
            }
        }

        public async Task HandleExceptionAsync (HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomeProblemDetails problem = new();

            switch (ex)
            {
                case BadRequestException BadRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomeProblemDetails()
                    {
                        Title = BadRequestException.Message,
                        Status = (int)statusCode,
                        Type = nameof(BadRequestException),
                        Detail = BadRequestException.InnerException?.Message,
                        Errorrs = BadRequestException.ValidationError
                    };

                    break;
            }
            httpContext.Response.StatusCode = (int)statusCode;
            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }
}
