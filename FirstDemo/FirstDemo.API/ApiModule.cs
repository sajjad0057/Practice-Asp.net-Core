using Autofac;
using FirstDemo.API.Models;

namespace FirstDemo.API;

public class ApiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {

        builder.RegisterType<CourseModel>().AsSelf();

        base.Load(builder);
    }
}
