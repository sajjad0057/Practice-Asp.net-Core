using FirstDemo.Infrastructure.BusinessObjects;

namespace FirstDemo.Infrastructure.Services
{
    public interface ICourseService
    {
        void CreateCourse(Course courseBO);
    }
}