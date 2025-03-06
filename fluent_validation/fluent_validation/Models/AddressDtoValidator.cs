using FluentValidation;

namespace fluent_validation.Models;

public class AddressValidator : AbstractValidator<AddressDto>
{
    public AddressValidator()
    {
        RuleFor(a => a.Street)
            .NotEmpty().WithMessage("Street is required")
            .MaximumLength(100).WithMessage("Street must be at most 100 characters");

        RuleFor(a => a.City)
            .NotEmpty().WithMessage("City is required")
            .MaximumLength(50).WithMessage("City must be at most 50 characters");

        RuleFor(a => a.State)
            .NotEmpty().WithMessage("State is required")
            .MaximumLength(50).WithMessage("State must be at most 50 characters");

        RuleFor(a => a.ZipCode)
            .NotEmpty().WithMessage("ZipCode is required")
            .Matches(@"^\d{5}(-\d{4})?$").WithMessage("Invalid ZipCode format (e.g., 12345 or 12345-6789)");
    }
}
