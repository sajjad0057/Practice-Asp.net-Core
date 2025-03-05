using FilterWithExHndlrMiddleware.Commons;

namespace FilterWithExHndlrMiddleware.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Call the next middleware in the pipeline
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");

            var errorResponse = new ErrorResponse
            {
                Errors = new List<string> { "An unexpected error occurred. Please try again later." }
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
