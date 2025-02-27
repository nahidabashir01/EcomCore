using FluentValidation;

namespace UserMicroservice.Dtos.Request.RequestValidator
{
    public class UserRegistrationDtoValidator : AbstractValidator<UserRegistrationDto>
    {
        public UserRegistrationDtoValidator() 
        {
            RuleFor(u => u.Name)
           .NotEmpty().WithMessage("Name is required.");

            RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(u => u.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\d{10,15}$").WithMessage("Phone number must contain only digits and be 10 to 15 digits long.");

            RuleFor(u => u.Password)
           .NotEmpty().WithMessage("Password is required.")
           .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
           .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
           .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
           .Matches(@"\d").WithMessage("Password must contain at least one digit.")
           .Matches(@"[\@\!\#\$\%\^\&\*\(\)]").WithMessage("Password must contain at least one special character.");
        }
    }
}
