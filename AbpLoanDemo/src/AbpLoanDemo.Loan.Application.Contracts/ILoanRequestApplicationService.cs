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

        Task<List<LoanRequestDto>> GetListAsync(LoanRequestQueryDto query);

        Task<LoanRequestDto> CreateAsync(LoanRequestCreateDto dto);

        Task<LoanRequestDto> AddPartner(Guid id, LoanRequestAddPartnerDto dto);

        Task<LoanRequestDto> UpdateScoreAsync(Guid id, LoanRequestSetScoreDto score);

        Task<LoanRequestDto> UpdateGuaranteeAsync(Guid id, LoanRequestSetGuaranteeDto guarantee);

        Task<LoanRequestDto> UpdateAmountAsync(Guid id, LoanRequestSetAmountDto dto);
    }
}