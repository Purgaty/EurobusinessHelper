using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;
using EurobusinessHelper.Application.Common.Exceptions;

namespace EurobusinessHelper.UI.ASP.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (EurobusinessException ex)
        {
            _logger.LogError(ex, "Application exception thrown");
            await HandleExceptionAsync(httpContext, ex.Code.ToString(), ex.Message, GetStatusCode(ex.Code));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unknown exception thrown");
            await HandleExceptionAsync(httpContext, "UnknownError", "Unknown error", HttpStatusCode.InternalServerError);
        }
    }

    private static HttpStatusCode GetStatusCode(EurobusinessExceptionCode exceptionCode)
    {
        return exceptionCode switch
        {
            EurobusinessExceptionCode.UnauthorizedUser => HttpStatusCode.Unauthorized,
            _ => HttpStatusCode.BadRequest
        };
    }

    private static async Task HandleExceptionAsync(HttpContext context,
        string errorCode,
        string errorMessage,
        HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(new ErrorDetails
        {
            ErrorCode = errorCode,
            Message = errorMessage
        }.ToString());
    }
    
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    private class ErrorDetails
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}