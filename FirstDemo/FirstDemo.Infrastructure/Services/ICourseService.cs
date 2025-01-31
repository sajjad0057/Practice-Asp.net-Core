using CourseBO = FirstDemo.Infrastructure.BusinessObjects.Course;


namespace FirstDemo.Infrastructure.Services;

public interface ICourseService
{
    Task CreateCourseAsync(CourseBO courseBO);
    void DeleteCourse(Guid id);
    void EditCourse(CourseBO courseBO);
    CourseBO GetCourse(Guid id);
    (int total, int totalDisplay, IList<CourseBO> records) GetCourses(int pageIndex,
    int pageSize, string searchText, string orderby);
}