using System.Threading;
using System.Threading.Tasks;
using AbpLoanDemo.Customer.Domain.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp;

namespace AbpLoanDemo.Customer.DbMigrator
{
    public class DbMigratorHostedService : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var application = AbpApplicationFactory.Create<AppCustomerDbMigratorModule>(options =>
            {
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
            });

            application.Initialize();

            await application
                .ServiceProvider
                .GetRequiredService<CustomerStoreMigrationService>()
                .MigrateAsync();

            application.Shutdown();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}