using System.ComponentModel.DataAnnotations;

namespace FirstDemo.Web.Areas.Admin.Models;

public class CourseCreateModel
{
    [Required]
    public string Title { get; set; }
    public double Fees { get; set; }
    public DateTime ClassStartDate { get; set; }

    internal async Task CreateCourseAsync()
    {
        throw new NotImplementedException();
    }
}
