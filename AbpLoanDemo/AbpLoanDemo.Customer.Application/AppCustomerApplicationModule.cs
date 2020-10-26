using AbpLoanDemo.Customer.Application.Contracts;
using AbpLoanDemo.Customer.Domain;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace AbpLoanDemo.Customer.Application
{
    [DependsOn(typeof(AbpAutoMapperModule))]
    [DependsOn(typeof(AppCustomerDomainModule))]
    [DependsOn(typeof(AppCustomerApplicationContractModule))]
    public class AppCustomerApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AppCustomerApplicationModule>();

            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<AppCustomerApplicationModule>(); });

            base.ConfigureServices(context);
        }
    }
}