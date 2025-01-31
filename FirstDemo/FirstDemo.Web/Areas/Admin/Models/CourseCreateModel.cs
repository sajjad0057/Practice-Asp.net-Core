using Autofac;
using AutoMapper;
using FirstDemo.Infrastructure.BusinessObjects;
using FirstDemo.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;

namespace FirstDemo.Web.Areas.Admin.Models;

public class CourseCreateModel : BaseModel
{
    [Required]
    public string Title { get; set; }
    public double Fees { get; set; }
    public DateTime ClassStartDate { get; set; }

    private ICourseService _courseService;
    private IMapper _mapper; 
  
    public CourseCreateModel() : base()
    {

    }
    public CourseCreateModel(ICourseService courseService, IMapper mapper)
    {
        _courseService = courseService;
        _mapper = mapper;
    }

    internal void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _courseService = _scope.Resolve<ICourseService>();
        _mapper = _scope.Resolve<IMapper>();
    }

    internal async Task CreateCourseAsync()
    {
        //Course course = new Course();
        //course.Name = Title;
        //course.Fees = Fees;
        //course.ClassStartDate = ClassStartDate;

        var course = _mapper.Map<Course>(this);

        await _courseService.CreateCourseAsync(course);

    }
}
