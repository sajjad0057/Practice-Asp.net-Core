using PracticeMVC.Infrastructure.BusinessObjects;

namespace PracticeMVC.Infrastructure.Services
{
    public interface ICourseService
    {
        void CreateCourse(Course courseBO);
    }
}