using System;
using AbpLoanDemo.Customer.Application.Contracts.Models.Dtos;
using FluentValidation;

namespace AbpLoanDemo.Customer.Application.Contracts.Models.Validators
{
    public class LoanRequestSetGuaranteeDtoValidator : AbstractValidator<LoanRequestSetGuaranteeDto>
    {
        public LoanRequestSetGuaranteeDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Cost).NotNull().GreaterThan(0);
            RuleFor(x => x.ExpiryDate).NotNull();
        }
    }
}