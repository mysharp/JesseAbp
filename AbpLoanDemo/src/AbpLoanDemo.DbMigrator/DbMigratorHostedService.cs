using System.Threading;
using System.Threading.Tasks;
using AbpLoanDemo.Customer.Domain.Data;
using AbpLoanDemo.Loan.Domain.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp;

namespace AbpLoanDemo.DbMigrator
{
    public class DbMigratorHostedService : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var application = AbpApplicationFactory.Create<AppDbMigratorModule>(options =>
            {
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
            });

            application.Initialize();

            await application
                .ServiceProvider
                .GetRequiredService<CustomerStoreMigrationService>()
                .MigrateAsync();


            await application
                .ServiceProvider
                .GetRequiredService<LoanStoreMigrationService>()
                .MigrateAsync();

            application.Shutdown();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}