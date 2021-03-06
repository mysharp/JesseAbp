﻿using AbpLoanDemo.Customer.Application.Contracts.Models.Dtos;
using AbpLoanDemo.Customer.Application.Contracts.Models.Etos;
using AbpLoanDemo.Customer.Domain.Entities;
using AutoMapper;

namespace AbpLoanDemo.Customer.Application.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Domain.Entities.Customer, CustomerDto>()
                .ForMember(x => x.Linkmen, x => x.MapFrom(p => p.Linkman))
                .ReverseMap();


            CreateMap<Domain.Entities.Customer, CustomerChangedEto>();

            CreateMap<Domain.Entities.Customer, CustomerEditDto>().ReverseMap();

            CreateMap<Linkman, LinkmanDto>().ReverseMap();
            CreateMap<Linkman, CustomerAddLinkmanDto>().ReverseMap();
        }
    }
}