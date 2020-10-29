using AbpLoanDemo.Loan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace AbpLoanDemo.Loan.EntityFrameworkCore
{
    [DependsOn(typeof(AbpEntityFrameworkCoreSqlServerModule))]
    public class AppLoanEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<LoanDbContext>(options =>
            {
                options.AddDefaultRepositories();

                options.Entity<LoanRequest>(
                    opt => opt.DefaultWithDetailsFunc =
                        q => q.Include(c => c.Applier)
                            //.Include(c => c.Guarantee)
                            .Include(c => c.Partners));
            });

            Configure<AbpDbContextOptions>(options => { options.UseSqlServer(); });
        }
    }
}