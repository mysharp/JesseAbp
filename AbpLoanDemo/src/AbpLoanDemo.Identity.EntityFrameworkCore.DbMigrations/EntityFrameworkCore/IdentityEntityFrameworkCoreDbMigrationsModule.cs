using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace AbpLoanDemo.Identity.EntityFrameworkCore
{
    [DependsOn(
        typeof(IdentityEntityFrameworkCoreModule)
        )]
    public class IdentityEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<IdentityMigrationsDbContext>();
        }
    }
}
