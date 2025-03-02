using app.Web.Models.DTO;
using FluentValidation;

namespace app.Web.Validator
{
    public class RegisterValidation : AbstractValidator<RegistrationRequestDTO>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .MinimumLength(9).WithMessage("Phone number must be at least 9 characters");

        }
    }
}
