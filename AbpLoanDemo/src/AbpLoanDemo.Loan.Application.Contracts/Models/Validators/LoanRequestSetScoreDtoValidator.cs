using AbpLoanDemo.Customer.Application.Contracts.Models.Dtos;
using FluentValidation;

namespace AbpLoanDemo.Customer.Application.Contracts.Models.Validators
{
    public class LoanRequestSetScoreDtoValidator : AbstractValidator<LoanRequestSetScoreDto>
    {
        public LoanRequestSetScoreDtoValidator()
        {
            RuleFor(x => x.Score).NotNull().GreaterThan(0).LessThanOrEqualTo(10);
        }
    }
}