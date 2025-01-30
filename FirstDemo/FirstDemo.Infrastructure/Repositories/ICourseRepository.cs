using CourseEO = FirstDemo.Infrastructure.Entities.Course;

namespace FirstDemo.Infrastructure.Repositories;

public interface ICourseRepository : IRepository<CourseEO, Guid>
{
    (IList<CourseEO> data, int total, int totalDisplay) GetCourses(int pageIndex, int pageSize, string searchText, string orderby);
}