using System.Text;
using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Infrastructure.Entities.IdentityEntities;
using FirstDemo.Infrastructure.Securities;
using FirstDemo.Infrastructure.Services.IdentityServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace FirstDemo.API;

public static class ServiceCollectionExtForCustomIndentityConfig
{
    public static IServiceCollection AddCustomIdentityServices(this IServiceCollection services, IConfiguration _configuration)
    {
        #region ForCustomizeIdentityManagement
        services
        .AddIdentity<ApplicationUser, ApplicationRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddUserManager<ApplicationUserManager>()
        .AddRoleManager<ApplicationRoleManager>()
        .AddSignInManager<ApplicationSignInManager>()
        .AddDefaultTokenProviders();

        ////For JWT Based Configuration for API project - 
        services.AddAuthentication()
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
            };
        });

        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("CourseViewRequirementPolicy", policy =>
            {
                policy.AuthenticationSchemes.Clear();
                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(new CourseViewRequirement());
            });

        });

        services.AddSingleton<IAuthorizationHandler, CourseViewRequirementHandler>();

        #endregion

        return services; // Returning IServiceCollection allows method chaining
    }
}
