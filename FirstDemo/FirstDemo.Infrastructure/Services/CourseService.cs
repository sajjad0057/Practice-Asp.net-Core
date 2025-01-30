using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Infrastructure.UnitOfWorks;
using CourseBO = FirstDemo.Infrastructure.BusinessObjects.Course;
using CourseEO = FirstDemo.Infrastructure.Entities.Course;

namespace FirstDemo.Infrastructure.Services;

public class CourseService(IApplicationUnitOfWork applicationUnitOfWork) : ICourseService
{
    private readonly IApplicationUnitOfWork _applicationUnitOfWork = applicationUnitOfWork;
    public void CreateCourse(CourseBO courseBO)
    {
        courseBO.SetProperClassStartDate();
        CourseEO courseEO = new CourseEO();
        courseEO.Title = courseBO.Name;
        courseEO.Fees = courseBO.Fees;
        courseEO.ClassStartDate = courseBO.ClassStartDate;

        _applicationUnitOfWork.Courses.Add(courseEO);
        _applicationUnitOfWork.Save();
    }

    public (int total, int totalDisplay, IList<CourseBO> records) GetCourses(int pageIndex,
    int pageSize, string searchText, string orderby)
    {
        (IList<CourseEO> data, int total, int totalDisplay) results = _applicationUnitOfWork
            .Courses.GetCourses(pageIndex, pageSize, searchText, orderby);


        IList<CourseBO> courses = new List<CourseBO>();

        foreach (CourseEO courseEO in results.data)
        {
            courses.Add(new CourseBO
            {
                Id = courseEO.Id,
                Name = courseEO.Title,
                Fees = courseEO.Fees,
                ClassStartDate = courseEO.ClassStartDate,
            });

        }

        return (results.total, results.totalDisplay, courses);


    }
}
