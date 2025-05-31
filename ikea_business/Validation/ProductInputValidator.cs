using FluentValidation;
using ikea_business.DTO;

namespace ikea_business.Validation
{
    public class ProductInputValidator : AbstractValidator<ProductInput>
    {
        public ProductInputValidator()
        {
            RuleFor(x => x.Article)
                .NotEmpty().WithMessage("Article is required.")
                .MaximumLength(50).WithMessage("Article cannot exceed 50 characters.")
                .Matches(@"^[A-Za-z0-9\-]+$").WithMessage("Article must contain only letters, numbers, and hyphens.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be greater than 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.")
                .Matches(@"^[A-Za-z\u0400-\u04FF0-9\s\-]+$").WithMessage("Name can contain letters, numbers, spaces, and hyphens.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.")
                .LessThanOrEqualTo(9999999.99m).WithMessage("Price is too large.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.")
                .Matches(@"^[A-Za-z\u0400-\u04FF0-9\s.,\-!?:;'()]*$").When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("Description contains invalid characters.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(0, 9.99m).When(x => x.Rating.HasValue)
                .WithMessage("Rating must be between 0 and 9.99.");

            RuleFor(x => x.Color)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.Color))
                .WithMessage("Color cannot exceed 50 characters.")
                .Matches(@"^[A-Za-z\u0400-\u04FF\s\-]+$").When(x => !string.IsNullOrEmpty(x.Color))
                .WithMessage("Color must contain only letters, spaces, and hyphens.");

            RuleFor(x => x.Dimensions)
                .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.Dimensions))
                .WithMessage("Dimensions cannot exceed 20 characters.")
                .Matches(@"^\d+x\d+x\d+$").When(x => !string.IsNullOrEmpty(x.Dimensions))
                .WithMessage("Dimensions must be in format WidthxHeightxDepth (e.g., 60x60x90).");

            RuleFor(x => x.Weight)
                .GreaterThan(0).When(x => x.Weight.HasValue)
                .WithMessage("Weight must be greater than 0.");

            RuleFor(x => x.Type)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.Type))
                .WithMessage("Type cannot exceed 50 characters.");

            RuleFor(x => x.CountryOfOrigin)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.CountryOfOrigin))
                .WithMessage("Country of Origin cannot exceed 50 characters.")
                .Matches(@"^[A-Za-z\u0400-\u04FF\s]+$").When(x => !string.IsNullOrEmpty(x.CountryOfOrigin))
                .WithMessage("Country of Origin must contain only letters and spaces.");

            RuleFor(x => x.PackageContents)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.PackageContents))
                .WithMessage("Package Contents cannot exceed 200 characters.");

            RuleFor(x => x.Warranty)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Warranty))
                .WithMessage("Warranty cannot exceed 100 characters.");

            RuleFor(x => x.Materials)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Materials))
                .WithMessage("Materials cannot exceed 200 characters.");

            RuleFor(x => x.MainImage)
                .MaximumLength(255).When(x => !string.IsNullOrEmpty(x.MainImage))
                .WithMessage("MainImage cannot exceed 255 characters.")
                .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                .WithMessage("MainImage must be a valid URL or relative path.");
        }
    }
}
