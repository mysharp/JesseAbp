using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbpLoanDemo.Customer.Application.Contracts;
using AbpLoanDemo.Customer.Application.Contracts.Models.Dtos;
using AbpLoanDemo.Customer.Domain.Entities;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AbpLoanDemo.Customer.Application
{
    public class CustomerApplicationService : ApplicationService, ICustomerApplicationService
    {
        private readonly IRepository<Domain.Entities.Customer> _customerRepository;

        public CustomerApplicationService(IRepository<Domain.Entities.Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>参数名称需要为id，不然动态代理会无法识别</remarks>
        /// <returns></returns>
        public async Task<CustomerDto> GetAsync(Guid id)
        {
            var customer = await _customerRepository.GetAsync(p => p.Id == id,true);

            return ObjectMapper.Map<Domain.Entities.Customer, CustomerDto>(customer);
        }

        public async Task<List<CustomerDto>> GetListAsync()
        {
            var customers = await _customerRepository.GetListAsync(true);

            return ObjectMapper.Map<List<Domain.Entities.Customer>, List<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> CreateAsync(CustomerCreateDto customer)
        {
            var entity = ObjectMapper.Map<CustomerCreateDto, Domain.Entities.Customer>(customer);
            var result = await _customerRepository.InsertAsync(entity);

            return ObjectMapper.Map<Domain.Entities.Customer, CustomerDto>(result);
        }

        public async Task<CustomerDto> AddLinkmanAsync(Guid id, LinkmanAddDto linkman)
        {
            var customer = await _customerRepository.GetAsync(c => c.Id == id);

            var linkmanEntity = ObjectMapper.Map<LinkmanAddDto, Linkman>(linkman);
            customer.AddLinkman(linkmanEntity);

            var updateCustomerResult = await _customerRepository.UpdateAsync(customer, true);

            return ObjectMapper.Map<Domain.Entities.Customer, CustomerDto>(updateCustomerResult);
        }
    }
}