using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace AbpLoanDemo.Loan.EntityFrameworkCore.DbMigrations
{
    [DependsOn(typeof(AppLoanEntityFrameworkCoreModule))]
    public class AppLoanEntityFrameworkCoreDbMigrationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<LoanDbMigrationContext>();
        }
    }
}