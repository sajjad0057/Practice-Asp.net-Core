using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Infrastructure.Entities.IdentityEntities;
using FirstDemo.Infrastructure.Securities;
using FirstDemo.Infrastructure.Services.IdentityServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace FirstDemo.API;

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

        #region forAutoMapperConfiguration
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        #endregion


        #region CustomizingIndentityRelatedConfig
        //using Ext. method for IServiceCollection to manage customizing identity related config -
        services.AddCustomIdentityServices(_configuration);
        #endregion


        #region ForConfiguring CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSites",
                builder =>
                {
                    builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
                });
        });
        #endregion


        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
      
    }

    public void Configure(WebApplication app)
    {
        Log.Information("Application Starting (API)...");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        #region CorsMiddlewareConfig
        ////For configuring CORS
        app.UseCors();
        #endregion

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}