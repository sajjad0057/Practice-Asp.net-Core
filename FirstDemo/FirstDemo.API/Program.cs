using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Reflection;
using FirstDemo.API;
using FirstDemo.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration));

try
{
    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    var assemblyName = Assembly.GetExecutingAssembly().FullName ??
        throw new InvalidOperationException("Does not found or exists assembly name");

    #region Autofac Config
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => {
        containerBuilder.RegisterModule(new ApiModule());
        containerBuilder.RegisterModule(new InfrastructureModule(connectionString,assemblyName));
    });
    #endregion

    //// Creating Startup class and move all code for service config to Startup class and executing these configure through Startup class using Startup class instance 
    var startup = new Startup(builder.Configuration);

    startup.ConfigureServices(builder.Services, connectionString, assemblyName);

    var app = builder.Build();

    startup.Configure(app);
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}