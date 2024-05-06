using System.Net;

namespace API.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    public readonly RequestDelegate _next;
    public readonly ILogger _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger logger)
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
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}