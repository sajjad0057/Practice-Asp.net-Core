using Autofac;
using FirstDemo.Web.Areas.Admin.Models;
using FirstDemo.Web.Models;

namespace FirstDemo.Web;

public class WebModule : Module
{

    //// **** Should not be used Model in Dependency Injection , although here we used Model Instance for create a Dependency Injection Examples **** 
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CourseModel>().As<ICourseModel>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CourseModel>().AsSelf();

        builder.RegisterType<CourseCreateModel>().AsSelf();
        builder.RegisterType<CourseListModel>().AsSelf();
        builder.RegisterType<CourseEditModel>().AsSelf();
        builder.RegisterType<RegisterModel>().AsSelf();
        builder.RegisterType<LoginModel>().AsSelf();

        base.Load(builder);
    }
}
