using Autofac;
using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Infrastructure.Repositories;
using FirstDemo.Infrastructure.Services;
using FirstDemo.Infrastructure.UnitOfWorks;

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

        builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
            .WithParameter("connectionString", _connectionString)
            .WithParameter("migrationAssemblyName", _migrationAssemblyName)
            .InstancePerLifetimeScope();

        builder.RegisterType<CourseService>().As<ICourseService>()
            .InstancePerLifetimeScope();
        builder.RegisterType<TimeService>().As<ITimeService>()
            .InstancePerLifetimeScope();
        builder.RegisterType<DataUtility>().As<IDataUtility>()
            .InstancePerLifetimeScope();


        builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CourseRepository>().As<ICourseRepository>()
            .InstancePerLifetimeScope();

        ////For FirstDemo.API project - 

        builder.RegisterType<TokenService>().As<ITokenService>()
            .InstancePerLifetimeScope();



        base.Load(builder);
    }
}
