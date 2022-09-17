using Autofac;
using PracticeMVC.Infrastructure.DbContexts;
using PracticeMVC.Infrastructure.Repositories;
using PracticeMVC.Infrastructure.Services;
using PracticeMVC.Infrastructure.UnitOfWorks;

namespace PracticeMVC.Infrastructure
{
    public class InfrastructureModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        public InfrastructureModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;

        }

        protected override void Load(ContainerBuilder builder)
        {



            //// InstancePerLifetimeScope() method keeps a single instance for single request .
            //// here pass parameter coz, ApplicationDbContext class constructor received two parameters . 


            /*
               Must be Binding ApplicationDbContext as AsSelf()..ApplicationDbContext Class have parametterized constructor
            -------------------------------------------------------------------------------------------------------------------------------------------
               Basically the main reason is when we create migrations we use --context ApplicationDbContext in migration command , 
               not Use IApplicationDbContext interface , so that Command Cannot resolve this interface . so need ApplicationDbContext AsSelf() Binding.
             */

            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();


            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();


            builder.RegisterType<CourseService>().As<ICourseService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CourseRepository>().As<ICourseRepository>()
                .InstancePerLifetimeScope();



            base.Load(builder);
        }
    }
}