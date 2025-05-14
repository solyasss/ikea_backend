using FluentValidation;
using ikea_business.DTO;

namespace ikea_business.Validation
{
    public class UserInputValidator : AbstractValidator<UserInput>
    {
        public UserInputValidator()
        {
            RuleFor(x => x.Email)   .EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName) .NotEmpty();
        }
    }
}