using fluent_validation.Models;
using FluentValidation;

namespace fluent_validation.Validators;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required");
        RuleFor(x => x.Age).InclusiveBetween(18, 60).WithMessage("Age must be between 18 and 60");
    }
}
