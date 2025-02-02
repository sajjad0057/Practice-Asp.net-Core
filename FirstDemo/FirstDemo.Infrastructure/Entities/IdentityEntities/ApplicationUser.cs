using Microsoft.AspNetCore.Identity;

namespace FirstDemo.Infrastructure.Entities.IdentityEntities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

