using AbpLoanDemo.Loan.Domain.Shared;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace AbpLoanDemo.Loan.Application.Contracts
{
    [DependsOn(typeof(AppLoanDomainSharedModule))]
    [DependsOn(
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpFluentValidationModule))]
    public class AppLoanApplicationContractModule : AbpModule
    {
    }
}