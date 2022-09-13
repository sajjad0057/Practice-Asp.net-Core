using PracticeMVC.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CourseBO = PracticeMVC.Infrastructure.BusinessObjects.Course;
using CourseEO = PracticeMVC.Infrastructure.Entities.Course;

namespace PracticeMVC.Infrastructure.Services
{
    public class CourseService : ICourseService
    {

        private IApplicationUnitOfWork _applicationUnitOfWork;

        public CourseService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public void CreateCourse(CourseBO courseBO)
        {
            courseBO.SetProperClassStartDate();

            CourseEO courseEO = new CourseEO();

            courseEO.Title = courseBO.Name;
            courseEO.Fees = courseBO.Fees;
            courseEO.ClassStartDate = courseBO.ClassStartDate;

            _applicationUnitOfWork.Courses.Add(courseEO);
            _applicationUnitOfWork.Save();
        }
    }
}
