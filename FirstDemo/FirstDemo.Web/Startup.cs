using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Web;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services, string connectionString, string assemblyName)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, m => m.MigrationsAssembly(assemblyName)));
        services.AddDatabaseDeveloperPageExceptionFilter();

        #region Config AutoMapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        #endregion

        //using Ext. method for IServiceCollection to manage customizing identity related config

        services.AddCustomIdentityServices(_configuration);

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
