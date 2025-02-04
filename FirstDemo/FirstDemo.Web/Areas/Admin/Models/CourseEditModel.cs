using Autofac;
using AutoMapper;
using FirstDemo.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;
using CourseBO = FirstDemo.Infrastructure.BusinessObjects.Course;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class CourseEditModel : BaseModel
    {

        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }
        public double Fees { get; set; }
        public DateTime ClassStartDate { get; set; }

        private ICourseService _courseService;
        private IMapper _mapper;

        public CourseEditModel() : base()
        {
        }

        public CourseEditModel(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _courseService = _scope.Resolve<ICourseService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        public void LoadData(Guid id)
        {
            CourseBO course = _courseService.GetCourse(id);

            if (course is not null)
            {
                _mapper.Map(course, this);
            }
        }

        public void EditCourse()
        {
            CourseBO courseBO = _mapper.Map<CourseBO>(this);
            _courseService.EditCourse(courseBO);
        }
    }
}
