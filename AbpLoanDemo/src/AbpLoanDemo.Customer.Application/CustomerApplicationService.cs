using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbpLoanDemo.Customer.Application.Contracts;
using AbpLoanDemo.Customer.Application.Contracts.Models.Dtos;
using AbpLoanDemo.Customer.Application.Contracts.Permissions;
using AbpLoanDemo.Customer.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

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
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>参数名称需要为id，不然动态代理会无法识别</remarks>
        /// <returns></returns>
        [Authorize(CustomerPermissions.Customer.Default)]
        public async Task<CustomerDto> GetAsync(Guid id)
        {
            var customer = await _customerRepository.GetAsync(p => p.Id == id);

            return ObjectMapper.Map<Domain.Entities.Customer, CustomerDto>(customer);
        }

        [Authorize(CustomerPermissions.Customer.Default)]
        public async Task<List<CustomerDto>> GetListAsync()
        {
            var customers = await _customerRepository.GetListAsync(true);

            return ObjectMapper.Map<List<Domain.Entities.Customer>, List<CustomerDto>>(customers);
        }

        [Authorize(CustomerPermissions.Customer.Create)]
        public async Task<CustomerDto> CreateAsync(CustomerCreateDto customer)
        {
            var entity = ObjectMapper.Map<CustomerCreateDto, Domain.Entities.Customer>(customer);

            using var uow = UnitOfWorkManager.Begin(new AbpUnitOfWorkOptions());
            var result = await _customerRepository.InsertAsync(entity);
            await uow.SaveChangesAsync();

            return ObjectMapper.Map<Domain.Entities.Customer, CustomerDto>(result);
        }

        [Authorize(CustomerPermissions.Customer.AddLinkman)]
        public async Task<CustomerDto> AddLinkmanAsync(Guid id, CustomerAddLinkmanDto linkman)
        {
            var customer = await _customerRepository.GetAsync(c => c.Id == id);

            var linkmanEntity = ObjectMapper.Map<CustomerAddLinkmanDto, Linkman>(linkman);
            customer.AddLinkman(linkmanEntity);

            var updateCustomerResult = await _customerRepository.UpdateAsync(customer, false);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<Domain.Entities.Customer, CustomerDto>(updateCustomerResult);
        }
    }
}