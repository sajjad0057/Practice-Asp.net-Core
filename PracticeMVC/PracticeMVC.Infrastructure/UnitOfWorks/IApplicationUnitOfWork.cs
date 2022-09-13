using PracticeMVC.Infrastructure.Repositories;

namespace PracticeMVC.Infrastructure.UnitOfWorks
{
    public interface IApplicationUnitOfWork
    {
        ICourseRepository Courses { get; }

        void Save();
    }
}