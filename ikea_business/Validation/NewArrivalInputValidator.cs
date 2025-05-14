using FluentValidation;
using ikea_business.DTO;

namespace ikea_business.Validation
{
    public class NewArrivalInputValidator : AbstractValidator<NewArrivalInput>
    {
        public NewArrivalInputValidator()
        {
            RuleFor(x => x.ImageUrl).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Text)    .NotEmpty().MaximumLength(100);
        }
    }
}