using AbpLoanDemo.Customer.Application.Contracts.Models.Dtos;
using AbpLoanDemo.Loan.Domain.Entities;
using AutoMapper;

namespace AbpLoanDemo.Loan.Application.Profiles
{
    public class LoanProfile : Profile
    {
        public LoanProfile()
        {
            CreateMap<LoanRequest, LoanRequestDto>().ReverseMap();
            CreateMap<LoanRequest, LoanRequestCreateDto>().ReverseMap();
            CreateMap<Applier, ApplierDto>().ReverseMap();
            CreateMap<Guarantee, GuaranteeDto>().ReverseMap();
        }
    }
}