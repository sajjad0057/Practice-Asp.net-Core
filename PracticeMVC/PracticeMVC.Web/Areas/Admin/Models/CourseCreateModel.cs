using Autofac;
using PracticeMVC.Infrastructure.BusinessObjects;
using PracticeMVC.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;

namespace PracticeMVC.Web.Areas.Admin.Models
{
    public class CourseCreateModel
    {
        [Required]
        public String Title { get; set; }
        public double Fees { get; set; }
        public DateTime ClassStartDate { get; set; }

        private ICourseService _CourseService;


        private ILifetimeScope _scope;


        public CourseCreateModel()
        {

        }


        public CourseCreateModel(ICourseService courseService)
        {
            _CourseService = courseService;
        }


        public void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _CourseService = _scope.Resolve<ICourseService>();
        }


        internal async Task CreateCourse()  //// we use async method here for, program wait here for successfully create Course instance
        {
            Course course = new Course();
            course.Name = Title;
            course.Fees = Fees;
            course.ClassStartDate = ClassStartDate;

            _CourseService.CreateCourse(course);
        }
    }
}
