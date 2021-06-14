using FluentValidation;

namespace OnlineBankingApp.Models
{
    public class CustomerValidator : AbstractValidator<Customer> {
        public CustomerValidator() {
            RuleFor(x => x.FirstName).Matches(@"^[A-Z]+[a-zA-Z]*$");
            RuleFor(x => x.MiddleName).Matches(@"^[A-Z]+[a-zA-Z]*$");
            RuleFor(x => x.LastName).Matches(@"^[A-Z]+[a-zA-Z]*$");
        }
    }
}