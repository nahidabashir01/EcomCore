using FluentValidation;

namespace UserMicroservice.Dtos.Request.RequestValidator
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator() 
        {
            RuleFor(u => u.Email)
           .NotEmpty().WithMessage("Email is required.");

            RuleFor(u => u.Password)
           .NotEmpty().WithMessage("Password is required.");
        }
    }
}
