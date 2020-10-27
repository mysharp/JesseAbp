using System;
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
            var loanRequests = await _loanRequestRepository.GetListAsync();

            return ObjectMapper.Map<List<LoanRequest>, List<LoanRequestDto>>(loanRequests);
        }

        public async Task<LoanRequestDto> CreateAsync(LoanRequestCreateDto loanRequest)
        {
            var customer = await CustomerApplicationService.GetAsync(loanRequest.CustomerId);
            if (customer == null)
                throw new AbpException("Customer is missing.");

            var applier = new Applier(customer.Id, customer.Name, customer.Phone, customer.IdNo);

            var loadRequest = new LoanRequest(applier);

            var entity = await _loanRequestRepository.InsertAsync(loadRequest, true);

            return ObjectMapper.Map<LoanRequest, LoanRequestDto>(entity);
        }

        public async Task<LoanRequestDto> AddPartner(Guid id, Guid partnerId)
        {
            var customer = await CustomerApplicationService.GetAsync(partnerId);
            if (customer == null)
                throw new AbpException("Partner is missing.");

            var loanRequest = await _loanRequestRepository.GetAsync(p => p.Id == id);
            var partner = new Applier(customer.Id, customer.Name, customer.Phone, customer.IdNo);
            loanRequest.AddPartner(partner);

            loanRequest = await _loanRequestRepository.UpdateAsync(loanRequest, true);

            return ObjectMapper.Map<LoanRequest, LoanRequestDto>(loanRequest);
        }

        public async Task<LoanRequestDto> SetScoreAsync(Guid id, decimal score)
        {
            var loanRequest = await _loanRequestRepository.GetAsync(p => p.Id == id);
            loanRequest.SetScore(score);

            loanRequest = await _loanRequestRepository.UpdateAsync(loanRequest, true);

            return ObjectMapper.Map<LoanRequest, LoanRequestDto>(loanRequest);
        }

        public async Task<LoanRequestDto> SetGuaranteeAsync(Guid id, GuaranteeDto guarantee)
        {
            var guaranteeEntity = ObjectMapper.Map<GuaranteeDto, Guarantee>(guarantee);

            var loanRequest = await _loanRequestRepository.GetAsync(p => p.Id == id);
            loanRequest.SetGuarantee(guaranteeEntity);

            loanRequest = await _loanRequestRepository.UpdateAsync(loanRequest, true);

            return ObjectMapper.Map<LoanRequest, LoanRequestDto>(loanRequest);
        }

        public async Task<LoanRequestDto> SetAmountAsync(Guid id, decimal amount)
        {
            var loanRequest = await _loanRequestRepository.GetAsync(p => p.Id == id);
            loanRequest.SetAmount(amount);

            loanRequest = await _loanRequestRepository.UpdateAsync(loanRequest, true);

            return ObjectMapper.Map<LoanRequest, LoanRequestDto>(loanRequest);
        }
    }
}