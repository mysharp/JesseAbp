using AbpLoanDemo.Customer.Domain;
using AbpLoanDemo.Customer.EntityFrameworkCore.DbMigrations;
using AbpLoanDemo.Loan.Domain;
using AbpLoanDemo.Loan.EntityFrameworkCore.DbMigrations;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AbpLoanDemo.DbMigrator
{
    [DependsOn(typeof(AbpAutofacModule))]
    [DependsOn(typeof(AppCustomerDomainModule), typeof(AppCustomerEntityFrameworkCoreDbMigrationModule))]
    [DependsOn(typeof(AppLoanDomainModule), typeof(AppLoanEntityFrameworkCoreDbMigrationModule))]
    public class AppDbMigratorModule : AbpModule
    {
    }
}