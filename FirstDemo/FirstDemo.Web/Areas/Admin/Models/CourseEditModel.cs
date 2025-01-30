using Autofac;
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
        //private IMapper _mapper;


        public CourseEditModel() : base()
        {

        }


        public CourseEditModel(ICourseService courseService)
        {
            _courseService = courseService;
            //_mapper = mapper;
        }

        //public void LoadData(Guid id)
        //{
        //    CourseBO course = _courseService.GetCourse(id);

        //    if (course != null)
        //    {
        //        _mapper.Map(course, this);
        //    }
        //}


        //public void EditCourse()
        //{
        //    CourseBO courseBO = _mapper.Map<CourseBO>(this);
        //    _courseService.EditCourse(courseBO);
        //}


        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _courseService = _scope.Resolve<ICourseService>();
            //_mapper = _scope.Resolve<IMapper>();
        }

        internal void LoadData(Guid id)
        {
            var course = _courseService.GetCourse(id);
            if(course is not null)
            {
                Id = id;
                Title = course.Name;
                Fees = course.Fees;
                ClassStartDate = course.ClassStartDate;
            }
        }

        internal void EditCourse()
        {
            CourseBO courseBO = new CourseBO();
            courseBO.Id = Id;
            courseBO.Name = Title;
            courseBO.Fees = Fees;
            courseBO.ClassStartDate = ClassStartDate;

            _courseService.EditCourse(courseBO);
        }
    }
}
