using FluentValidation;
using ikea_business.DTO;

namespace ikea_business.Validation
{
    public class UserInputValidator : AbstractValidator<UserInput>
    {
        public UserInputValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(50).WithMessage("First Name cannot exceed 50 characters.")
                .Matches(@"^[A-Za-z\u0400-\u04FF]+$").WithMessage("First Name must contain only letters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(50).WithMessage("Last Name cannot exceed 50 characters.")
                .Matches(@"^[A-Za-z\u0400-\u04FF]+$").WithMessage("Last Name must contain only letters.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Birth Date is required.")
                .LessThan(DateTime.Today).WithMessage("Birth Date must be in the past.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country cannot exceed 50 characters.")
                .Matches(@"^[A-Za-z\u0400-\u04FF\s]+$").WithMessage("Country must contain only letters and spaces.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(100).WithMessage("Address cannot exceed 100 characters.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+\d{9,15}$").WithMessage("Phone number must start with + and contain 9 to 15 digits.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

            RuleFor(x => x.AvatarUrl)
                .NotEmpty().WithMessage("Avatar URL is required.")
                .MaximumLength(255).WithMessage("Avatar URL cannot exceed 255 characters.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                .WithMessage("Avatar URL must be a valid URL.");
        }
    }
}
