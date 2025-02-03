using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Infrastructure.Entities.IdentityEntities;
using FirstDemo.Infrastructure.Securities;
using FirstDemo.Infrastructure.Services.IdentityServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FirstDemo.Web;

public static class ServiceCollectionExtForCustomIndentityConfig
{
    public static IServiceCollection AddCustomIdentityServices(this IServiceCollection services)
    {
        #region ForDefaultIdentityManagement
        ////services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        ////    .AddEntityFrameworkStores<ApplicationDbContext>();
        #endregion

        // Configure Identity
        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserManager<ApplicationUserManager>()
            .AddRoleManager<ApplicationRoleManager>()
            .AddSignInManager<ApplicationSignInManager>()
            .AddDefaultTokenProviders();

        // Configure Cookie Authentication
        services
            .AddAuthentication()
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = new PathString("/Account/Login");
                options.AccessDeniedPath = new PathString("/Account/Login");
                options.LogoutPath = new PathString("/Account/Logout");
                options.Cookie.Name = "FirstDemoPortal.Identity";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
            });

        // Configure Identity Options
        services.Configure<IdentityOptions>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        });

        // Configure Policy-Based Authorization
        services.AddAuthorization(options =>
        {
            options.AddPolicy("CourseManagementPolicy", policy =>
            {
                policy.RequireAuthenticatedUser();
                //// by these here perform OR operations on Roles
                policy.RequireRole("Admin", "Teacher");

                //// If need AND operation in Roles
                //policy.RequireRole("Admin");
                //policy.RequireRole("Teacher");

            });

            options.AddPolicy("CourseManagementPolicy", policy =>
            {
                policy.RequireAuthenticatedUser();
                //// by these here perform OR operations on Roles
                policy.RequireRole("Admin", "Teacher");

                //// If need AND operation in Roles
                //policy.RequireRole("Admin");
                //policy.RequireRole("Teacher");

            });

            //options.AddPolicy("CourseViewPolicy", policy =>
            //{
            //    policy.RequireAuthenticatedUser();
            //    //// here "ViewCourseClaim" is ClaimType and  "true" is value
            //    policy.RequireClaim("ViewCourseClaim", "true");  //// Claim type: "ViewCourseClaim", Claim value: "true"
            //});

            options.AddPolicy("CourseDeletePolicy", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("CourseDeletClaim", "true");
            });


            //// Another Way to Manage Claim based polices with requirment handler
            options.AddPolicy("CourseViewRequirementPolicy", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(new CourseViewRequirement());
            });
        });

        ////Bind these for resolved CourseViewRequirementHandler.
        services.AddSingleton<IAuthorizationHandler, CourseViewRequirementHandler>();

        return services; // Returning IServiceCollection allows method chaining
    }
}

