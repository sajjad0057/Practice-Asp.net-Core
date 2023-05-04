using GlobalExceptionHandling.Exceptions;
using GlobalExceptionHandling.Models;
using System.Net;

namespace GlobalExceptionHandling.Middlewares;

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
			_logger.LogError(ex,ex.Message,ex.StackTrace);
			await HandleException(context, ex);
		}
    }

	private static Task HandleException(HttpContext context, Exception ex) 
	{
		int statusCode = StatusCodes.Status500InternalServerError;

		switch (ex)
		{
			case NotFoundException _:
                statusCode = StatusCodes.Status404NotFound;
                break;
            case BadRequestException _:
                statusCode = StatusCodes.Status400BadRequest;
                break;
            case DivideByZeroException _:
                statusCode = StatusCodes.Status400BadRequest;
                break;

			//// you can define some more exception according to need
        }

		var errorResponse = new ErrorResponse
		{
			StatusCode = statusCode,
			Message = ex.Message,
		};

		context.Response.ContentType= "application/json";
		context.Response.StatusCode = statusCode;

		return context.Response.WriteAsync(errorResponse.ToString());
	}
}

// Extension method for this middleware

public static class ExceptionMiddlewareExtension
{
	public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
	{
		app.UseMiddleware<ExceptionMiddleware>();
	}
}
