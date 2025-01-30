namespace FirstDemo.Infrastructure.Entities;

public class CourseRegistration
{
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
}
