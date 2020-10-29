using AbpLoanDemo.Loan.Domain.Shared;
using Volo.Abp.Modularity;

namespace AbpLoanDemo.Loan.Application.Contracts
{
    [DependsOn(
        typeof(AppLoanDomainSharedModule)
    )]
    public class AppLoanApplicationContractModule : AbpModule
    {
    }
}