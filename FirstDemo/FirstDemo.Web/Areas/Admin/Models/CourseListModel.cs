using Autofac;
using FirstDemo.Infrastructure.Services;
using FirstDemo.Web.Models;

namespace FirstDemo.Web.Areas.Admin.Models;

public class CourseListModel : BaseModel
{
    private ICourseService _courseService;

    public CourseListModel(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _courseService = _scope.Resolve<ICourseService>();
    }

    public object? GetPagedCourses(DataTablesAjaxRequestModel model)
    {

        var data = _courseService.GetCourses(
            model.PageIndex,
            model.PageSize,
            model.SearchText,
            model.GetSortText(new string[] { "Title", "Fees", "ClassStartDate" }));

        return new
        {
            recordsTotal = data.total,
            recordsFiltered = data.totalDisplay,
            data = (from record in data.records
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

    internal void DeleteCourse(Guid id)
    {
        _courseService.DeleteCourse(id);
    }
}
