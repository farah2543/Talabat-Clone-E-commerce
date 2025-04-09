using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;

namespace E_commerce.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate? _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

            }
            catch (Exception e)
            {
                _logger.LogError($"Something Went Wrong :{e}");

                await HandleExceptionAsync(httpContext, e);

            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            httpContext.Response.StatusCode = e switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound, 
                _ => (int)HttpStatusCode.InternalServerError       
            };


            var response = new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = e.Message
            }.ToString();

            await httpContext.Response.WriteAsync(response);




        }
    }

    

}
