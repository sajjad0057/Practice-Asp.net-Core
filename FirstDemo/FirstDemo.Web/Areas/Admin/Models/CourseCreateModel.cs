using Autofac;
using AutoMapper;
using FirstDemo.Infrastructure.BusinessObjects;
using FirstDemo.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;

namespace FirstDemo.Web.Areas.Admin.Models;

public class CourseCreateModel : BaseModel
{
    // For Using ServerSide Validations - 
    [Required(ErrorMessage = "Title must be provided"), StringLength(200, ErrorMessage = "Title should be less than 200 characters")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Fees must be provided"), Range(1000, 50000, ErrorMessage = "Fees range must have 1000 to 50000")]
    public double Fees { get; set; }
    [Required(ErrorMessage = "Valid date time must be provided")]
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
