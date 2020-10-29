using AbpLoanDemo.Customer.Domain.Shared;
using Volo.Abp.Modularity;

namespace AbpLoanDemo.Customer.Domain
{
    [DependsOn(typeof(AppCustomerDomainSharedModule))]
    public class AppCustomerDomainModule : AbpModule
    {
    }
}