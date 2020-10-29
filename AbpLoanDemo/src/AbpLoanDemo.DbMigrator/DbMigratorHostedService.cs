using System.Threading;
using System.Threading.Tasks;
using AbpLoanDemo.Customer.Domain.Data;
using AbpLoanDemo.Identity.Data;
using AbpLoanDemo.Loan.Domain.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp;

namespace AbpLoanDemo.DbMigrator
{
    public class DbMigratorHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
        }

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
                .GetRequiredService<IdentityDbMigrationService>()
                .MigrateAsync();

            await application
                .ServiceProvider
                .GetRequiredService<CustomerStoreMigrationService>()
                .MigrateAsync();


            await application
                .ServiceProvider
                .GetRequiredService<LoanStoreMigrationService>()
                .MigrateAsync();

            application.Shutdown();

            _hostApplicationLifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}