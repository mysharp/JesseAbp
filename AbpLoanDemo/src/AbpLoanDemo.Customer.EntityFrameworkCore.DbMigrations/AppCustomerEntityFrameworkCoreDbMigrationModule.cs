using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace AbpLoanDemo.Customer.EntityFrameworkCore.DbMigrations
{
    [DependsOn(typeof(AppCustomerEntityFrameworkCoreModule))]
    public class AppCustomerEntityFrameworkCoreDbMigrationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<CustomerDbMigrationContext>();
        }
    }
}