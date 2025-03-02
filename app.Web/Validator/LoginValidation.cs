using app.Web.Models.DTO;
using FluentValidation;


namespace app.Web.Validator
{
    public class LoginValidation : AbstractValidator<LoginRequestDTO>
    {
        public LoginValidation()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }
}
