using System;
using AbpLoanDemo.Customer.Application.Contracts.Models.Dtos;
using FluentValidation;

namespace AbpLoanDemo.Customer.Application.Contracts.Models.Validators
{
    public class LoanRequestAddPartnerDtoValidator : AbstractValidator<LoanRequestAddPartnerDto>
    {
        public LoanRequestAddPartnerDtoValidator()
        {
            RuleFor(x => x.PartnerId).NotNull().NotEqual(Guid.Empty);
        }
    }
}