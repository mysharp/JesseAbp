﻿using AbpLoanDemo.Customer.Application.Contracts;
using AbpLoanDemo.Customer.Application.Contracts.Models.Dtos;
using AbpLoanDemo.Customer.Application.Contracts.Permissions;
using AbpLoanDemo.Loan.Application.Contracts;
using AbpLoanDemo.Loan.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace AbpLoanDemo.Loan.Application
{
    [Authorize(LoanPermissions.LoanRequest.Default)]
    public class LoanRequestApplicationService : ApplicationService, ILoanRequestApplicationService
    {
        private readonly IRepository<LoanRequest,Guid> _loanRequestRepository;
        private readonly IUnitOfWorkManager _uowManager;

        public LoanRequestApplicationService(ICustomerApplicationService customerApplicationService,
            IRepository<LoanRequest, Guid> loanRequestRepository, IUnitOfWorkManager uowManager)
        {
            CustomerApplicationService = customerApplicationService;
            _loanRequestRepository = loanRequestRepository;
            _uowManager = uowManager;
        }

        protected ICustomerApplicationService CustomerApplicationService { get; set; }

        public virtual async Task<LoanRequestDto> GetAsync(Guid id)
        {
            var loanRequest = await _loanRequestRepository.GetAsync(p => p.Id == id);

            return ObjectMapper.Map<LoanRequest, LoanRequestDto>(loanRequest);
        }

        public virtual async Task<List<LoanRequestDto>> GetListAsync(LoanRequestQueryDto query)
        {
            var loanRequests = _loanRequestRepository.WithDetails().Where(p => true);

            if (query.CustomerId!= Guid.Empty)
                loanRequests = loanRequests.Where(p => p.Applier.CustomerId == query.CustomerId || p.Partners.Any(c=>c.CustomerId == query.CustomerId));

            var result = await AsyncExecuter.ToListAsync(loanRequests);

            return ObjectMapper.Map<List<LoanRequest>, List<LoanRequestDto>>(result);
        }

        [Authorize(LoanPermissions.LoanRequest.Create)]
        public virtual async Task<LoanRequestDto> CreateAsync(LoanRequestCreateDto dto)
        {
            var customer = await CustomerApplicationService.GetAsync(dto.CustomerId);
            if (customer == null)
                throw new AbpException("Customer is missing.");

            var applier = new Applier(customer.Id, customer.Name, customer.Phone, customer.IdNo);

            var loadRequest = new LoanRequest(applier);

            var entity = await _loanRequestRepository.InsertAsync(loadRequest);

            return ObjectMapper.Map<LoanRequest, LoanRequestDto>(entity);
        }

        [Authorize(LoanPermissions.LoanRequest.AddPartner)]
        public virtual async Task<LoanRequestDto> AddPartner(Guid id, LoanRequestAddPartnerDto dto)
        {
            var customer = await CustomerApplicationService.GetAsync(dto.PartnerId);
            if (customer == null)
                throw new AbpException("Partner is missing.");

            var loanRequest = await _loanRequestRepository.GetAsync(p => p.Id == id);
            var partner = new Applier(customer.Id, customer.Name, customer.Phone, customer.IdNo);
            loanRequest.AddPartner(partner);

            loanRequest = await _loanRequestRepository.UpdateAsync(loanRequest);

            return ObjectMapper.Map<LoanRequest, LoanRequestDto>(loanRequest);
        }

        [Authorize(LoanPermissions.LoanRequest.UpdateScore)]
        public virtual async Task<LoanRequestDto> UpdateScoreAsync(Guid id, LoanRequestSetScoreDto dto)
        {
            var loanRequest = await _loanRequestRepository.GetAsync(p => p.Id == id);
            loanRequest.SetScore(dto.Score);

            loanRequest = await _loanRequestRepository.UpdateAsync(loanRequest);

            return ObjectMapper.Map<LoanRequest, LoanRequestDto>(loanRequest);
        }

        [Authorize(LoanPermissions.LoanRequest.UpdateGuarantee)]
        public virtual async Task<LoanRequestDto> UpdateGuaranteeAsync(Guid id, LoanRequestSetGuaranteeDto dto)
        {
            var guaranteeEntity = ObjectMapper.Map<LoanRequestSetGuaranteeDto, Guarantee>(dto);

            var loanRequest = await _loanRequestRepository.GetAsync(p => p.Id == id);
            loanRequest.SetGuarantee(guaranteeEntity);

            loanRequest = await _loanRequestRepository.UpdateAsync(loanRequest);

            return ObjectMapper.Map<LoanRequest, LoanRequestDto>(loanRequest);
        }

        [Authorize(LoanPermissions.LoanRequest.UpdateAmount)]
        public virtual async Task<LoanRequestDto> UpdateAmountAsync(Guid id, LoanRequestSetAmountDto dto)
        {
            var loanRequest = await _loanRequestRepository.GetAsync(p => p.Id == id);
            loanRequest.SetAmount(dto.Amount);

            loanRequest = await _loanRequestRepository.UpdateAsync(loanRequest);

            return ObjectMapper.Map<LoanRequest, LoanRequestDto>(loanRequest);
        }
    }
}