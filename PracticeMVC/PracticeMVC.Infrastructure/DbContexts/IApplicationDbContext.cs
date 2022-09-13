using Microsoft.EntityFrameworkCore;
using PracticeMVC.Infrastructure.Entities;

namespace PracticeMVC.Infrastructure.DbContexts
{
    public interface IApplicationDbContext
    {
        DbSet<Course> Courses { get; set; }
    }
}