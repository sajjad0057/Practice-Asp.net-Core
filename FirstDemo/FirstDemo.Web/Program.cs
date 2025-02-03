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
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found."); ;
    var assemblyName = Assembly.GetExecutingAssembly().FullName;

    // Add Autofac Dependency Injection
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new WebModule());
        containerBuilder.RegisterModule(
            new InfrastructureModule(connectionString, assemblyName)
            );
    });

    //// Creating Startup class and move some code to these class and executing these configure through Startup class using Startup class instance 
    // Create `Startup` instance
    var startup = new Startup(builder.Configuration);
    
    startup.ConfigureServices(builder.Services);

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
