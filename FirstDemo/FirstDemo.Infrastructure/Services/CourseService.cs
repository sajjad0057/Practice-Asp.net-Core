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
}
