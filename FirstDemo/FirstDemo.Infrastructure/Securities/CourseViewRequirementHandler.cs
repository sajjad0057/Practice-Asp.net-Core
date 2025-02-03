using Microsoft.AspNetCore.Authorization;

namespace FirstDemo.Infrastructure.Securities
{
    public class CourseViewRequirementHandler :
          AuthorizationHandler<CourseViewRequirement>
    {
        protected override Task HandleRequirementAsync(
               AuthorizationHandlerContext context,
               CourseViewRequirement requirement)
        {
            if (context.User.HasClaim(x => x.Type == "ViewCourseClaim" && x.Value == "true"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
