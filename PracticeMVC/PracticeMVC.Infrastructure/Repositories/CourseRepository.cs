using Microsoft.EntityFrameworkCore;
using PracticeMVC.Infrastructure.DbContexts;
using PracticeMVC.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeMVC.Infrastructure.Repositories
{
    public class CourseRepository : Repository<Course, Guid> , ICourseRepository
    {
        public CourseRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }
    }
}
