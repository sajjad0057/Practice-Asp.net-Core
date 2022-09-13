using Autofac;
using PracticeMVC.Web.Areas.Admin.Models;

namespace PracticeMVC.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //// Bind Your dependency here : 
            builder.RegisterType<CourseCreateModel>().AsSelf();

            base.Load(builder);
        }
    }
}
