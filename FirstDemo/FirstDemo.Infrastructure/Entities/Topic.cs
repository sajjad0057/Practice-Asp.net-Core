namespace FirstDemo.Infrastructure.Entities;

public class Topic : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
}
