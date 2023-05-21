using TestEnumValidation.Middlewares;

namespace TestEnumValidation.Extensions;

public static class ExceptionMiddlewareExtension
{
    public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}
