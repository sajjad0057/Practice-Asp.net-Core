using AutoMapper;
using FirstDemo.Infrastructure.Exceptions;
using FirstDemo.Infrastructure.UnitOfWorks;
using CourseBO = FirstDemo.Infrastructure.BusinessObjects.Course;
using CourseEO = FirstDemo.Infrastructure.Entities.Course;

namespace FirstDemo.Infrastructure.Services;

public class CourseService(IMapper mapper, IApplicationUnitOfWork applicationUnitOfWork) : ICourseService
{
    private readonly IApplicationUnitOfWork _applicationUnitOfWork = applicationUnitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task CreateCourseAsync(CourseBO courseBO)
    {
        var count = _applicationUnitOfWork.Courses.GetCount(x => x.Title == courseBO.Name);

        if (count > 0)
        {
            throw new DuplicateException("Course Title Already Exists !");
        }

        courseBO.SetProperClassStartDate();

        var courseEntity = _mapper.Map<CourseEO>(courseBO);

        await Task.Run(() => _applicationUnitOfWork.Courses.Add(courseEntity));

        _applicationUnitOfWork.Save();

    }

    public (int total, int totalDisplay, IList<CourseBO> records) GetCourses(int pageIndex,
    int pageSize, string searchText, string orderby)
    {
        (IList<CourseEO> data, int total, int totalDisplay) results = _applicationUnitOfWork
            .Courses.GetCourses(pageIndex, pageSize, searchText, orderby);

        //IList<CourseBO> courses = new List<CourseBO>();

        //foreach (CourseEO courseEO in results.data)
        //{
        //    courses.Add(_mapper.Map<CourseBO>(courseEO));

        //}

        var courses = _mapper.Map<List<CourseBO>>(results.data);

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

        return _mapper.Map<CourseBO>(courseEO);
    }

    public void EditCourse(CourseBO courseBO)
    {
        var courseEO = _applicationUnitOfWork.Courses.GetById(courseBO.Id);
        if(courseEO is not null)
        {
            _mapper.Map(courseBO, courseEO);

            _applicationUnitOfWork.Save();
        }
        else
        {
            throw new InvalidOperationException("Course was not found");
        }
    }
}
