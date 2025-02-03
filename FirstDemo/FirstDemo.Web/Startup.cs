using System.Reflection;
using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Infrastructure.Entities.IdentityEntities;
using FirstDemo.Infrastructure.Services.IdentityServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        var assemblyName = Assembly.GetExecutingAssembly().FullName;

        // Configure DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, m => m.MigrationsAssembly(assemblyName)));

        services.AddDatabaseDeveloperPageExceptionFilter();

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

        // Configure Policy-Based Authorization
        services.AddAuthorization(options =>
        {
            options.AddPolicy("CourseManagementPolicy", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("Admin", "Teacher");
            });
        });

        services.AddControllersWithViews();
    }

    public void Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
