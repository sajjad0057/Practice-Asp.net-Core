using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracticeMVC.Infrastructure;
using PracticeMVC.Infrastructure.DbContexts;
using PracticeMVC.Web;
using PracticeMVC.Web.Data;
using Serilog;
using Serilog.Events;
using System.Reflection;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    var assemblyName = Assembly.GetExecutingAssembly().FullName;


    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();


    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.AddControllersWithViews();

    #region Autofac Configuration

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); //// by this here, added autofac as dependency injection framework in our app.
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        //// here , can load one /more module that's bind dependency 
        containerBuilder.RegisterModule(new WebModule()); //// it's for Web project dependency binding

        containerBuilder.RegisterModule(new InfrastructureModule(connectionString,assemblyName)); //// it's for Infrastructure project dependency binding
    });
    #endregion

    #region Serilog(Logger) Configuraion

    builder.Host.UseSerilog((ctx, lc) => lc
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(builder.Configuration)
    );

    #endregion


    var app = builder.Build();



    // Here Log static class being accessible after initially app Build successfully .

    Log.Information("Application Starting .");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


    app.MapRazorPages();

    app.Run();

}
catch(Exception ex)
{
    Log.Fatal(ex, "Application Start-up Failed . Error Occur");
}
finally
{
    Log.CloseAndFlush();
}