using Autofac.Extensions.DependencyInjection;
using Autofac;
using FirstDemo.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FirstDemo.Web;
using Serilog;
using Serilog.Events;
using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region Serilog (Logger) Configuration 

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration)
    );

#endregion

try
{

    Log.Information("Application starting");
    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    var assemblyName = Assembly.GetExecutingAssembly().FullName;

    #region Autofac Configuration

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());  //// by this here, added autofac as dependency injection framework with asp.net core app
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        //// here , can load one / more module that need for binding .
        containerBuilder.RegisterModule(new WebModule());  //// it's for Web project dependency binding

        containerBuilder.RegisterModule(new InfrastructureModule(connectionString,
            assemblyName)); //// it's for Infrastructure project dependency binding
    });

    #endregion

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString, m => m.MigrationsAssembly(assemblyName)));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.AddControllersWithViews();

    builder.Services.AddTransient<ICourseModel, CourseModel>();

    var app = builder.Build();

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
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed .");
}
finally
{
    Log.CloseAndFlush();
}


