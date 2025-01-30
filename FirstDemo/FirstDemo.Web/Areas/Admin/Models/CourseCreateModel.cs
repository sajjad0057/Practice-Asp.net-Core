using Autofac;
using FirstDemo.Infrastructure.BusinessObjects;
using FirstDemo.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;

namespace FirstDemo.Web.Areas.Admin.Models;

public class CourseCreateModel
{
    [Required]
    public string Title { get; set; }
    public double Fees { get; set; }
    public DateTime ClassStartDate { get; set; }

    private ICourseService _courseService;
    private ILifetimeScope _scope;
    public CourseCreateModel()
    {
        
    }
    public CourseCreateModel(ICourseService courseService)
    {
        _courseService = courseService;
    }

    internal void ResolveDependency(ILifetimeScope scope)
    {
        _scope = scope;
        _courseService = _scope.Resolve<ICourseService>();
    }

    internal async Task CreateCourseAsync()
    {
        Course course = new Course();
        course.Name = Title;
        course.Fees = Fees;
        course.ClassStartDate = ClassStartDate;

        _courseService.CreateCourse(course);

    }
}
