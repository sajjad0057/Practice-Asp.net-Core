namespace FirstDemo.Infrastructure.Entities;

public class Result : IEntity<Guid>
{
    public Guid Id { get; set; }
    public double CGPA { get; set; }
    public string SubjectName { get; set; }
}
