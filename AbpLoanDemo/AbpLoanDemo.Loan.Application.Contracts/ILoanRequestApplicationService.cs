using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbpLoanDemo.Customer.Application.Contracts.Models.Dtos;
using Volo.Abp.Application.Services;

namespace AbpLoanDemo.Loan.Application.Contracts
{
    public interface ILoanRequestApplicationService : IApplicationService
    {
        Task<LoanRequestDto> GetAsync(Guid id);

        Task<List<LoanRequestDto>> GetListAsync();

        Task<LoanRequestDto> CreateAsync(LoanRequestCreateDto loanRequest);

        Task<LoanRequestDto> SetScoreAsync(Guid id, decimal score);

        Task<LoanRequestDto> SetGuaranteeAsync(Guid id, GuaranteeDto guarantee);

        Task<LoanRequestDto> SetAmountAsync(Guid id, decimal amount);
    }
}