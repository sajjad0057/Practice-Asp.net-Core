using Autofac;
using FirstDemo.Infrastructure.DbContexts;

namespace FirstDemo.Infrastructure;

public class InfrastructureModule(string connectionString, string migrationAssemblyName) : Module
{
    private readonly string _connectionString = connectionString;
    private readonly string _migrationAssemblyName = migrationAssemblyName;


    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ApplicationDbContext>()
            .AsSelf()
            .WithParameter("connectionString", _connectionString)
            .WithParameter("migrationAssemblyName", _migrationAssemblyName)
            .InstancePerLifetimeScope();


        base.Load(builder);
    }
}
