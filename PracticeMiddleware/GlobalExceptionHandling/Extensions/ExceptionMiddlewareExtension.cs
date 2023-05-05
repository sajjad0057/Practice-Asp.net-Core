using GlobalExceptionHandling.Middlewares;

namespace GlobalExceptionHandling.Extensions;


// Extension method for this middleware
public static class ExceptionMiddlewareExtension
{
    public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}