using Autofac.Extensions.DependencyInjection;
using Autofac;
using FirstDemo.Infrastructure;
using FirstDemo.Web;
using Serilog;
using System.Reflection;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog Logging
builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration)
);

try
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    var assemblyName = Assembly.GetExecutingAssembly().FullName ??
        throw new InvalidOperationException("Does not found or exists assembly name");

    #region Autofac Configuration

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());  //// by this here, added autofac as dependency injection framework with asp.net core app
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        //// here , can load one / more module that need for binding .
        containerBuilder.RegisterModule(new WebModule());  //// it's for Web project dependency binding

        containerBuilder.RegisterModule(new InfrastructureModule(connectionString, assemblyName)); //// it's for Infrastructure project dependency binding
    });

    #endregion


    //// Creating Startup class and move all code for service config to Startup class and executing these configure through Startup class using Startup class instance 
    var startup = new Startup(builder.Configuration);
    
    startup.ConfigureServices(builder.Services, connectionString, assemblyName);

    var app = builder.Build();

    startup.Configure(app);
}
catch(Exception ex)
{
    Log.Fatal(ex, "Application start-up failed .");
}
finally
{
    Log.CloseAndFlush();
}
