namespace FirstDemo.Infrastructure.Entities;

public class Student : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double Cgpa { get; set; }
    public IList<CourseRegistration> StudentCourses { get; set; }
}
