using AbpLoanDemo.Customer.Application.Contracts.Models.Dtos;
using AbpLoanDemo.Customer.Domain.Entities;
using AutoMapper;

namespace AbpLoanDemo.Customer.Application.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Domain.Entities.Customer, CustomerDto>().ReverseMap();
            CreateMap<Linkman, LinkmanDto>().ReverseMap();
        }
    }
}