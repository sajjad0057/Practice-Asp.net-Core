using FirstDemo.Infrastructure.Exceptions;
using FirstDemo.Infrastructure.UnitOfWorks;
using CourseBO = FirstDemo.Infrastructure.BusinessObjects.Course;
using CourseEO = FirstDemo.Infrastructure.Entities.Course;

namespace FirstDemo.Infrastructure.Services;

public class CourseService(IApplicationUnitOfWork applicationUnitOfWork) : ICourseService
{
    private readonly IApplicationUnitOfWork _applicationUnitOfWork = applicationUnitOfWork;
    public void CreateCourse(CourseBO courseBO)
    {
        var count = _applicationUnitOfWork.Courses.GetCount(x => x.Title == courseBO.Name);

        if (count > 0)
        {
            throw new DuplicateException("Course Title Already Exists !");
        }

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

    public void DeleteCourse(Guid id)
    {
       _applicationUnitOfWork.Courses.Remove(id);
       _applicationUnitOfWork.Save();
    }

    public CourseBO GetCourse(Guid id)
    {
        var courseEO = _applicationUnitOfWork.Courses.GetById(id);

        var courseBO = new CourseBO();

        courseBO.Name = courseEO.Title;
        courseBO.Fees = courseEO.Fees;
        courseBO.ClassStartDate = courseEO.ClassStartDate;

        return courseBO;      
    }

    public void EditCourse(CourseBO courseBO)
    {
        var courseEO = _applicationUnitOfWork.Courses.GetById(courseBO.Id);
        if(courseEO is not null)
        {
            courseEO.Title = courseBO.Name;
            courseEO.Fees = courseBO.Fees;
            courseEO.ClassStartDate = courseBO.ClassStartDate;

            _applicationUnitOfWork.Save();
        }
        else
        {
            throw new InvalidOperationException("Course was not found");
        }
    }
}
