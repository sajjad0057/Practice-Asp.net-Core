using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Infrastructure.UnitOfWorks
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public ICourseRepository Courses { get; private set; }
        public ApplicationUnitOfWork(IApplicationDbContext dbContext,
            ICourseRepository courseRepository) : base((DbContext)dbContext)
        {
            Courses = courseRepository;
        }
    }
}
