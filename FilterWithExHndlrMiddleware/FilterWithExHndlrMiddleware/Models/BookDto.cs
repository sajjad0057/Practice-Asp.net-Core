using System.ComponentModel.DataAnnotations;

namespace FilterWithExHndlrMiddleware.Models;


public class BookDto
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Author name is required")]
    public string Author { get; set; }
    [Required]
    public string Description { get; set; }
    [Range(5, 1000)]
    public int Pages { get; set; }
}
