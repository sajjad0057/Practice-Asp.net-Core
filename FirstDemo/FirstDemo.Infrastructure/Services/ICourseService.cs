using CourseBO = FirstDemo.Infrastructure.BusinessObjects.Course;


namespace FirstDemo.Infrastructure.Services;

public interface ICourseService
{
    Task CreateCourseAsync(CourseBO courseBO);
    void DeleteCourse(Guid id);
    void EditCourse(CourseBO courseBO);
    Task<CourseBO> GetCourseAsync(Guid id);
    CourseBO GetCourse(Guid id);
    Task<IList<CourseBO>> GetCoursesAsync();
    (int total, int totalDisplay, IList<CourseBO> records) GetCourses(int pageIndex,
    int pageSize, string searchText, string orderby);
}