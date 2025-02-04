using System.Text;
using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Infrastructure.Entities.IdentityEntities;
using FirstDemo.Infrastructure.Securities;
using FirstDemo.Infrastructure.Services.IdentityServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace FirstDemo.Web;

public static class ServiceCollectionExtForCustomIndentityConfig
{
    public static IServiceCollection AddCustomIdentityServices(this IServiceCollection services, IConfiguration _configuration)
    {
        #region ForDefaultIdentityManagement
        ////services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        ////    .AddEntityFrameworkStores<ApplicationDbContext>();
        #endregion

        #region ForCustomizeIdentityManagement
        // Configure Identity
        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserManager<ApplicationUserManager>()
            .AddRoleManager<ApplicationRoleManager>()
            .AddSignInManager<ApplicationSignInManager>()
            .AddDefaultTokenProviders();

        // Configure Cookie Based Authentication for web project -
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
            })

            //// For Configuring JWT Token - connecting with API project and get courses from api project
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

            //// For authorized by JWT Token when make request to API Controller in Web Project - 
            options.AddPolicy("ApiRequirementPolicy", policy =>
            {
                policy.AuthenticationSchemes.Clear();
                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);   //// By this declare this policy authenticated by JWT not by default.
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(new ApiRequirement());
            });
        });



        ////Bind these for resolved CourseViewRequirementHandler & ApiRequirementHandler.
        services.AddSingleton<IAuthorizationHandler, CourseViewRequirementHandler>();
        services.AddSingleton<IAuthorizationHandler, ApiRequirementHandler>();

        #endregion

        return services; // Returning IServiceCollection allows method chaining
    }
}

