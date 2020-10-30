﻿using AbpLoanDemo.Customer.Application.Contracts;
using AbpLoanDemo.Customer.Application.Contracts.Models.Dtos;
using AbpLoanDemo.Customer.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbpLoanDemo.Customer.Application.Contracts.Permissions;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Security.Claims;
using Volo.Abp.Uow;

namespace AbpLoanDemo.Customer.Application
{
    [Authorize(CustomerPermissions.Customer.Default)]
    public class CustomerApplicationService : ApplicationService, ICustomerApplicationService
    {
        private readonly IRepository<Domain.Entities.Customer> _customerRepository;
        private readonly ICurrentPrincipalAccessor _accessor;

        public CustomerApplicationService(IRepository<Domain.Entities.Customer> customerRepository, ICurrentPrincipalAccessor accessor)
        {
            _customerRepository = customerRepository;
            _accessor = accessor;
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>参数名称需要为id，不然动态代理会无法识别</remarks>
        /// <returns></returns>
        public virtual async Task<CustomerDto> GetAsync(Guid id)
        {
            var user = _accessor.Principal;
            var currentUser = CurrentUser;
            var customer = await _customerRepository.GetAsync(p => p.Id == id);

            return ObjectMapper.Map<Domain.Entities.Customer, CustomerDto>(customer);
        }

        public virtual async Task<List<CustomerDto>> GetListAsync()
        {
            var customers = await _customerRepository.GetListAsync(true);

            return ObjectMapper.Map<List<Domain.Entities.Customer>, List<CustomerDto>>(customers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <remarks>
        ///     为了使使用动态代理/拦截系统来执行验证,方法必须是 virtual 的
        /// </remarks>
        /// <returns></returns>
        [Authorize(CustomerPermissions.Customer.Create)]
        public virtual async Task<CustomerDto> CreateAsync(CustomerCreateDto customer)
        {
            var entity = ObjectMapper.Map<CustomerCreateDto, Domain.Entities.Customer>(customer);

            using var uow = UnitOfWorkManager.Begin(new AbpUnitOfWorkOptions());
            var result = await _customerRepository.InsertAsync(entity);
            await uow.SaveChangesAsync();

            return ObjectMapper.Map<Domain.Entities.Customer, CustomerDto>(result);
        }

        [Authorize(CustomerPermissions.Customer.AddLinkman)]
        public virtual async Task<CustomerDto> AddLinkmanAsync(Guid id, CustomerAddLinkmanDto linkman)
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