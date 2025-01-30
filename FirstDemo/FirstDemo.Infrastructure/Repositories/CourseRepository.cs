using FirstDemo.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using CourseEO =  FirstDemo.Infrastructure.Entities.Course;

namespace FirstDemo.Infrastructure.Repositories;

public class CourseRepository : Repository<CourseEO, Guid>, ICourseRepository
{
    public CourseRepository(IApplicationDbContext context) : base((DbContext)context)
    {
    }


    public (IList<CourseEO> data, int total, int totalDisplay) GetCourses(int pageIndex,
        int pageSize, string searchText, string orderby)
    {
        (IList<CourseEO> data, int total, int totalDisplay) results =
            GetDynamic(x => x.Title.Contains(searchText), orderby, "Topics,CourseStudents", pageIndex, pageSize, true);

        return results;
    }
}
