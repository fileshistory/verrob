using System.Net;
using Shared.Models.Exceptions;

namespace Web.Middlewares.ErrorsHandler;

public class ErrorsHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorsHandlerMiddleware> _logger;

    public ErrorsHandlerMiddleware(RequestDelegate next, ILogger<ErrorsHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception exception)
        {
            string message = exception.Message;
                
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
                
            switch (exception)
            {
                case UnauthorizedException:
                    response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    break;
                default:
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    message = "Internal Server Error";
                    break;
            }

            await response.WriteAsync(new ErrorPayload
            {
                Message = message,
                StatusCode = response.StatusCode
            }.ToString());
            
            _logger.LogError(exception.ToString());
        }
    }
}