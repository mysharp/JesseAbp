using AbpLoanDemo.Loan.Domain.Shared;
using Volo.Abp.Modularity;

namespace AbpLoanDemo.Loan.Domain
{
    [DependsOn(typeof(AppLoanDomainSharedModule))]
    public class AppLoanDomainModule : AbpModule
    {
    }
}