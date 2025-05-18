using FluentValidation;
using ikea_business.DTO;

namespace ikea_business.Validation
{
    public class ProductInputValidator : AbstractValidator<ProductInput>
    {
        public ProductInputValidator()
        {
            RuleFor(x => x.Article)       .NotEmpty().MaximumLength(50);
            RuleFor(x => x.CategoryId)    .GreaterThan(0);
            RuleFor(x => x.Name)          .NotEmpty().MaximumLength(100);
            RuleFor(x => x.Price)         .GreaterThan(0);
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.Rating)        .InclusiveBetween(0, 9.99m).When(x => x.Rating.HasValue);
        }
    }
}