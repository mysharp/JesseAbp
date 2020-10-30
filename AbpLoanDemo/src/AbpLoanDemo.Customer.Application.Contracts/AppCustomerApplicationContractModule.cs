using AbpLoanDemo.Customer.Domain.Shared;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace AbpLoanDemo.Customer.Application.Contracts
{
    [DependsOn(typeof(AppCustomerDomainSharedModule))]
    [DependsOn(
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpFluentValidationModule))]
    public class AppCustomerApplicationContractModule : AbpModule
    {
    }
}