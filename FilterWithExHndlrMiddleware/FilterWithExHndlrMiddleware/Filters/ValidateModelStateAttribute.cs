using FilterWithExHndlrMiddleware.Commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterWithExHndlrMiddleware.Filters;

public class ValidateModelStateAttribute : ActionFilterAttribute
{
    /// <summary>
    /// This method is called before the action method is executed
    /// </summary>
    /// <param name="context"></param>
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errorResponse = new ErrorResponse();

            foreach (var error in context.ModelState.Values)
            {
                foreach (var subError in error.Errors)
                {
                    errorResponse.Errors.Add(subError.ErrorMessage);
                }
            }

            context.Result = new BadRequestObjectResult(errorResponse);
        }
    }

    /// <summary>
    /// This method is called after the action method is executed
    /// </summary>
    /// <param name="context"></param>
    public override void OnResultExecuted(ResultExecutedContext context)
    {
        ////TODO:  You can handle actions after the action executes, if needed
    }
}
