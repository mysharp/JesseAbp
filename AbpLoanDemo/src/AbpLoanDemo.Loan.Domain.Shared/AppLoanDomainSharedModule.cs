using AbpLoanDemo.Customer.Domain.Shared.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace AbpLoanDemo.Loan.Domain.Shared
{
    [DependsOn(typeof(AbpLocalizationModule))]
    public class AppLoanDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AppLoanDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<LoanResource>("en")
                    .AddVirtualJson("/Localization/Loan");

                options.DefaultResourceType = typeof(LoanResource);
            });

        }
    }
}
