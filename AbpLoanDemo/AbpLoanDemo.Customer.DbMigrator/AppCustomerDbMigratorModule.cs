using AbpLoanDemo.Customer.Domain;
using AbpLoanDemo.Customer.EntityFrameworkCore.DbMigrations;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AbpLoanDemo.Customer.DbMigrator
{
    [DependsOn(typeof(AbpAutofacModule), typeof(AppCustomerDomainModule), typeof(AppCustomerEntityFrameworkCoreDbMigrationModule))]
    public class AppCustomerDbMigratorModule : AbpModule
    {
        
    }
}