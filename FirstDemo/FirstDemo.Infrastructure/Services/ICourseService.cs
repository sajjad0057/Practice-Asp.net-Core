using CourseBO = FirstDemo.Infrastructure.BusinessObjects.Course;


namespace FirstDemo.Infrastructure.Services;

public interface ICourseService
{
    void CreateCourse(CourseBO courseBO);
    (int total, int totalDisplay, IList<CourseBO> records) GetCourses(int pageIndex,
    int pageSize, string searchText, string orderby);
}