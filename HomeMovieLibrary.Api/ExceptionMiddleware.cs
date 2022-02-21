using System.Net;
using HomeMovieLibrary.Api.Models;

namespace HomeMovieLibrary.Api;

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
        catch (Exception ex)
        {
            if (ex is TaskCanceledException || ex is OperationCanceledException)
            {
                _logger.LogInformation("Request cancelled: {requestMethod} {requestPath}", httpContext.Request.Method, httpContext.Request.Path);
                return;
            }

            _logger.LogError(ex, "Request ERROR: {requestMethod} {requestPath}", httpContext.Request.Method, httpContext.Request.Path);
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(new ErrorResponse()
        {
            StatusCode = context.Response.StatusCode,
            Message = "Something went horribly wrong!"
        }.ToString());
    }
}
