using System.Text.Json;

namespace TestEnumValidation.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, ex.StackTrace);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        int statusCode = StatusCodes.Status500InternalServerError;

        switch (ex)
        {
            case DivideByZeroException _:
                statusCode = StatusCodes.Status400BadRequest;
                break;

                //// you can define some more exception according to need
        }

        var errorResponse = new
        {
            StatusCode = statusCode,
            Message = ex.Message,
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var x = context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));

        return x;
    }
}
