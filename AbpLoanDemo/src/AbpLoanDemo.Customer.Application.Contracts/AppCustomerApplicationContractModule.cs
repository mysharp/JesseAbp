using AbpLoanDemo.Customer.Domain.Shared;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace AbpLoanDemo.Customer.Application.Contracts
{
    [DependsOn(
        typeof(AppCustomerDomainSharedModule),
            typeof(AbpPermissionManagementApplicationContractsModule)
    )]
    public class AppCustomerApplicationContractModule : AbpModule
    {
    }
}