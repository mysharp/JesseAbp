using AbpLoanDemo.Loan.Application.Contracts;
using AbpLoanDemo.Loan.Domain;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace AbpLoanDemo.Loan.Application
{
    [DependsOn(typeof(AbpAutoMapperModule))]
    [DependsOn(typeof(AppLoanDomainModule))]
    [DependsOn(typeof(AppLoanApplicationContractModule))]
    public class AppLoanApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AppLoanApplicationModule>();

            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<AppLoanApplicationModule>(); });

            base.ConfigureServices(context);
        }
    }
}