using System.ComponentModel.DataAnnotations;
using TestEnumValidation.Enums;

namespace TestEnumValidation.Models
{
    public class Person
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public FavouriteColor Color { get; set; }
    }
}
