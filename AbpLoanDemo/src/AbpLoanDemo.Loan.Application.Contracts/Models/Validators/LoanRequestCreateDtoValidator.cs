using System;
using AbpLoanDemo.Customer.Application.Contracts.Models.Dtos;
using FluentValidation;

namespace AbpLoanDemo.Customer.Application.Contracts.Models.Validators
{
    public class LoanRequestCreateDtoValidator : AbstractValidator<LoanRequestCreateDto>
    {
        public LoanRequestCreateDtoValidator()
        {
            RuleFor(x => x.CustomerId).NotNull().NotEqual(Guid.Empty);
        }
    }
}