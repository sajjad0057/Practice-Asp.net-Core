using Microsoft.EntityFrameworkCore;
using PracticeMVC.Infrastructure.DbContexts;
using PracticeMVC.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeMVC.Infrastructure.UnitOfWorks
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public ICourseRepository Courses { get; private set; }
        public ApplicationUnitOfWork(IApplicationDbContext dbContext,ICourseRepository courseRepository) : 
            base((DbContext)dbContext)
        {
            Courses = courseRepository;
        }
    }
}
