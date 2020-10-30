using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbpLoanDemo.Customer.Application.Contracts.Models.Dtos;
using Volo.Abp.Application.Services;

namespace AbpLoanDemo.Customer.Application.Contracts
{
    public interface ICustomerApplicationService : IApplicationService
    {
        Task<CustomerDto> GetAsync(Guid id);

        Task<List<CustomerDto>> GetListAsync();

        Task<CustomerDto> CreateAsync(CustomerEditDto customer);

        Task<CustomerDto> UpdateAsync(Guid id, CustomerEditDto customer);

        Task<CustomerDto> AddLinkmanAsync(Guid id, CustomerAddLinkmanDto linkman);
    }
}