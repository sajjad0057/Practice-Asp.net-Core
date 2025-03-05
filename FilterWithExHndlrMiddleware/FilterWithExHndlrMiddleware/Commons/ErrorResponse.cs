using Microsoft.AspNetCore.Mvc;

namespace FilterWithExHndlrMiddleware.Commons;

public class ErrorResponse
{
    public bool Success { get; set; } = false;
    public List<string> Errors { get; set; } = new List<string>();

    public static IActionResult GenerateErrorResponse(ActionContext context)
    {
        var errorResponse = new ErrorResponse();
        foreach (var error in context.ModelState.Values)
        {
            foreach (var subError in error.Errors)
            {
                errorResponse.Errors.Add(subError.ErrorMessage);
            }
        }

        return new BadRequestObjectResult(errorResponse);
    }
}
