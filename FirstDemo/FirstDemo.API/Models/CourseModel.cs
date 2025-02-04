using Autofac;
using AutoMapper;
using CourseBO = FirstDemo.Infrastructure.BusinessObjects.Course;
using FirstDemo.Infrastructure.Services;
using FirstDemo.Infrastructure.Models;

namespace FirstDemo.API.Models;

public class CourseModel : BaseModel
{
    private ICourseService _courseService;
    private IMapper _mapper;

    public Guid Id { get; set; }
    public string Title { get; set; }
    public double Fees { get; set; }
    public DateTime ClassStartDate { get; set; }

    public CourseModel() : base()
    {

    }

    public CourseModel(IMapper mapper, ICourseService courseService)
    {
        _mapper = mapper;
        _courseService = courseService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _courseService = _scope.Resolve<ICourseService>();
        _mapper = _scope.Resolve<IMapper>();
    }
    internal async Task<IEnumerable<CourseBO>> GetCoursesAsync()
    {
        return await _courseService.GetCoursesAsync();
    }

    internal async Task<CourseBO?> GetCourseByIdAsync(Guid id)
    {
        return await _courseService.GetCourseAsync(id);
    }

    internal void CreateCourse()
    {
        throw new NotImplementedException();
    }

    internal async Task CreateCourseAsync()
    {
        CourseBO courseBO = _mapper.Map<CourseBO>(this);

        await _courseService.CreateCourseAsync(courseBO);
    }

    internal async Task UpdateCourseAsync()
    {
        var courseBO = _mapper.Map<CourseBO>(this);
        _courseService.EditCourse(courseBO);

        await Task.CompletedTask;
    }

    internal async Task DeleteCourseAsync(Guid id)
    {
        _courseService.DeleteCourse(id);
        await Task.CompletedTask;
    }

    /// <summary>
    /// For Working with Datatables from Web project
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    internal object? GetPagedCourses(DataTablesAjaxRequestModel model)
    {

        var data = _courseService?.GetCourses(
            model.PageIndex,
            model.PageSize,
            model.SearchText,
            model.GetSortText(new string[] { "Title", "Fees", "ClassStartDate" }));

        return new
        {
            recordsTotal = data?.total,
            recordsFiltered = data?.totalDisplay,
            data = (from record in data?.records
                    select new string[]
                    {
                                record.Name,
                                record.Fees.ToString(),
                                record.ClassStartDate.ToString(),
                                record.Id.ToString()
                    }
                ).ToArray()
        };
    }
}
