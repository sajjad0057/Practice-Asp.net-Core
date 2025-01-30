using Autofac;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class BaseModel
    {

        protected ILifetimeScope _scope;

        //// Must be keep here empty Constructor 
        public BaseModel()
        {

        }

        public virtual void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
        }
    }
}
