﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbpLoanDemo.Customer.Application.Contracts;
using AbpLoanDemo.Customer.Application.Contracts.Models.Dtos;
using AbpLoanDemo.Loan.Application.Contracts;
using AbpLoanDemo.Loan.Domain.Entities;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AbpLoanDemo.Loan.Application
{
    public class LoanRequestApplicationService : ApplicationService, ILoanRequestApplicationService
    {
        private readonly IRepository<LoanRequest> _loanRequestRepository;

        public LoanRequestApplicationService(ICustomerApplicationService customerApplicationService,
            IRepository<LoanRequest> loanRequestRepository)
        {
            CustomerApplicationService = customerApplicationService;
            _loanRequestRepository = loanRequestRepository;
        }

        protected ICustomerApplicationService CustomerApplicationService { get; set; }

        public async Task<LoanRequestDto> GetAsync(Guid id)
        {
            var loanRequest = await _loanRequestRepository.GetAsync(p => p.Id == id);

            return ObjectMapper.Map<LoanRequest, LoanRequestDto>(loanRequest);
        }

        public async Task<List<LoanRequestDto>> GetListAsync()
        {
            var loanRequests = await _loanRequestRepository.GetListAsync(true);

            return ObjectMapper.Map<List<LoanRequest>, List<LoanRequestDto>>(loanRequests);
        }

        public async Task<LoanRequestDto> CreateAsync(LoanRequestCreateDto dto)
        {
            var customer = await CustomerApplicationService.GetAsync(dto.CustomerId);
            if (customer == null)
                throw new AbpException("Customer is missing.");

            var applier = new Applier(customer.Id, customer.Name, customer.Phone, customer.IdNo);

            var loadRequest = new LoanRequest(applier);

            var entity = await _loanRequestRepository.InsertAsync(loadRequest, true);

            return ObjectMapper.Map<LoanRequest, LoanRequestDto>(entity);
        }

        public async Task<LoanRequestDto> AddPartner(Guid id, LoanRequestAddPartnerDto dto)
        {
            var customer = await CustomerApplicationService.GetAsync(dto.PartnerId);
            if (customer == null)
                throw new AbpException("Partner is missing.");

            var loanRequest = await _loanRequestRepository.GetAsync(p => p.Id == id);
            var partner = new Applier(customer.Id, customer.Name, customer.Phone, customer.IdNo);
            loanRequest.AddPartner(partner);

            loanRequest = await _loanRequestRepository.UpdateAsync(loanRequest, true);

            return ObjectMapper.Map<LoanRequest, LoanRequestDto>(loanRequest);
        }

        public async Task<LoanRequestDto> UpdateScoreAsync(Guid id, LoanRequestSetScoreDto dto)
        {
            var loanRequest = await _loanRequestRepository.GetAsync(p => p.Id == id);
            loanRequest.SetScore(dto.Score);

            loanRequest = await _loanRequestRepository.UpdateAsync(loanRequest, true);

            return ObjectMapper.Map<LoanRequest, LoanRequestDto>(loanRequest);
        }

        public async Task<LoanRequestDto> UpdateGuaranteeAsync(Guid id, LoanRequestSetGuaranteeDto dto)
        {
            var guaranteeEntity = ObjectMapper.Map<LoanRequestSetGuaranteeDto, Guarantee>(dto);

            var loanRequest = await _loanRequestRepository.GetAsync(p => p.Id == id);
            loanRequest.SetGuarantee(guaranteeEntity);

            loanRequest = await _loanRequestRepository.UpdateAsync(loanRequest, true);

            return ObjectMapper.Map<LoanRequest, LoanRequestDto>(loanRequest);
        }

        public async Task<LoanRequestDto> UpdateAmountAsync(Guid id, LoanRequestSetAmountDto dto)
        {
            var loanRequest = await _loanRequestRepository.GetAsync(p => p.Id == id);
            loanRequest.SetAmount(dto.Amount);

            loanRequest = await _loanRequestRepository.UpdateAsync(loanRequest, true);

            return ObjectMapper.Map<LoanRequest, LoanRequestDto>(loanRequest);
        }
    }
}