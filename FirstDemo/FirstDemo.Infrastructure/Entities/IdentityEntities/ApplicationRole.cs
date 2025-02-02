using Microsoft.AspNetCore.Identity;

namespace FirstDemo.Infrastructure.Entities.IdentityEntities;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole() : base()
    {      
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
    }
}
