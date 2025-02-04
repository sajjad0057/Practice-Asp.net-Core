using Autofac;

namespace FirstDemo.API.Models
{
    public class BaseModel
    {
        protected ILifetimeScope _scope;

        public BaseModel()
        {
            
        }
        public virtual void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
        }
    }
}