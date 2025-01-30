using FirstDemo.Infrastructure.Repositories;

namespace FirstDemo.Infrastructure.UnitOfWorks
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        ICourseRepository Courses { get; }
    }
}