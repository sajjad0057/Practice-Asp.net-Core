using FirstDemo.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Infrastructure.DbContexts
{
    public interface IApplicationDbContext
    {
        DbSet<Course> Courses { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<Result> Results { get; set; }
    }
}