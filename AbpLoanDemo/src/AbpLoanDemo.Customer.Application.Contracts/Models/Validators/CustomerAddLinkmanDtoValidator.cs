using AbpLoanDemo.Customer.Application.Contracts.Models.Dtos;
using FluentValidation;

namespace AbpLoanDemo.Customer.Application.Contracts.Models.Validators
{
    public class CustomerAddLinkmanDtoValidator : AbstractValidator<CustomerAddLinkmanDto>
    {
        public CustomerAddLinkmanDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.IdNo).NotEmpty().MaximumLength(50);
        }
    }
}