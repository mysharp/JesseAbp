using System;
using AbpLoanDemo.Customer.Application.Contracts.Models.Dtos;
using FluentValidation;

namespace AbpLoanDemo.Customer.Application.Contracts.Models.Validators
{
    public class LoanRequestSetAmountDtoValidator : AbstractValidator<LoanRequestSetAmountDto>
    {
        public LoanRequestSetAmountDtoValidator()
        {
            RuleFor(x => x.Amount).NotNull().GreaterThan(0);
        }
    }
}