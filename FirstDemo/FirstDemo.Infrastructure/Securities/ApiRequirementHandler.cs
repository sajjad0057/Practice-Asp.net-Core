using Microsoft.AspNetCore.Authorization;

namespace FirstDemo.Infrastructure.Securities
{
    public class ApiRequirementHandler : AuthorizationHandler<ApiRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            ApiRequirement requirement)
        {
            if (context.User.HasClaim(x => x.Type == "ViewCourseClaim" && x.Value == "true"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
