using AbpLoanDemo.Customer.Domain;
using AbpLoanDemo.Customer.EntityFrameworkCore.DbMigrations;
using AbpLoanDemo.Identity;
using AbpLoanDemo.Identity.EntityFrameworkCore;
using AbpLoanDemo.Loan.Domain;
using AbpLoanDemo.Loan.EntityFrameworkCore.DbMigrations;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace AbpLoanDemo.DbMigrator
{
    [DependsOn(typeof(AbpAutofacModule))]
    [DependsOn(typeof(AppCustomerDomainModule), typeof(AppCustomerEntityFrameworkCoreDbMigrationModule))]
    [DependsOn(typeof(AppLoanDomainModule), typeof(AppLoanEntityFrameworkCoreDbMigrationModule))]
    [DependsOn(typeof(IdentityEntityFrameworkCoreDbMigrationsModule),
    typeof(IdentityApplicationContractsModule))]
    public class AppDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}