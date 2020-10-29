using System;
using AbpLoanDemo.Customer.Domain.Shared.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace AbpLoanDemo.Customer.Domain.Shared
{
    [DependsOn(typeof(AbpLocalizationModule))]
    public class AppCustomerDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AppCustomerDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<CustomerResource>("en")
                    .AddVirtualJson("/Localization/Customer");

                options.DefaultResourceType = typeof(CustomerResource);
            });

        }
    }
}
