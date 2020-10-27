using AbpLoanDemo.Loan.Domain;
using AbpLoanDemo.Loan.EntityFrameworkCore.DbMigrations;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AbpLoanDemo.Loan.DbMigrator
{
    [DependsOn(typeof(AbpAutofacModule), typeof(AppLoanDomainModule), typeof(AppLoanEntityFrameworkCoreDbMigrationModule))]
    public class AppLoanDbMigratorModule : AbpModule
    {
        
    }
}