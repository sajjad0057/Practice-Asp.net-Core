using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Controllers;


namespace fluent_validation.Middleware;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IServiceProvider services)
    {
        var request = context.Request;

        // Ensure request is a JSON request with a body
        if (request.ContentLength > 0 && request.HasJsonContentType())
        {
            using var reader = new StreamReader(request.Body);
            var body = await reader.ReadToEndAsync();
            request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(body));

            // Retrieve endpoint metadata
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
                if (controllerActionDescriptor != null)
                {
                    // Get the first action parameter (assuming single DTO in body)
                    var parameter = controllerActionDescriptor.MethodInfo.GetParameters().FirstOrDefault();
                    if (parameter != null)
                    {
                        var dtoType = parameter.ParameterType;

                        // Deserialize request body into the detected DTO type
                        var dto = JsonSerializer.Deserialize(body, dtoType, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        if (dto != null)
                        {
                            // Resolve FluentValidation validator for the DTO type
                            var validatorType = typeof(IValidator<>).MakeGenericType(dtoType);
                            var validator = services.GetService(validatorType) as IValidator;

                            if (validator != null)
                            {
                                var validationResult = await validator.ValidateAsync(new ValidationContext<object>(dto));

                                if (!validationResult.IsValid)
                                {
                                    context.Response.StatusCode = 400;
                                    await context.Response.WriteAsJsonAsync(new { 
                                        IsSuccess = false, 
                                        StatusCode = 400, 
                                        Errors = validationResult.Errors.Select(e => e.ErrorMessage) 
                                    });

                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        await _next(context);
    }
}
