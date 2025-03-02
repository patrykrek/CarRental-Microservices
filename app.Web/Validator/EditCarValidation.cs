using app.Web.Models.DTO;
using FluentValidation;

namespace app.Web.Validator
{
    public class EditCarValidation : AbstractValidator<UpdateCarDTO>
    {
        public EditCarValidation()
        {
            RuleFor(x => x.Make)
                .NotEmpty().WithMessage("Make is required");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required");

            RuleFor(x => x.PricePerDay)
                .NotEmpty().WithMessage("Price is required")
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(x => x.Year)
                .NotEmpty().WithMessage("Year is required");
        }
    }
}
