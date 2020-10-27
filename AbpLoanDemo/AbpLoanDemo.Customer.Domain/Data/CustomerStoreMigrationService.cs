using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.DependencyInjection;

namespace AbpLoanDemo.Customer.Domain.Data
{
    public class CustomerStoreMigrationService : ITransientDependency
    {
        private readonly ICustomerStoreSchemaMigrator _dbSchemaMigrator;

        public CustomerStoreMigrationService(ICustomerStoreSchemaMigrator dbSchemaMigrator)
        {
            _dbSchemaMigrator = dbSchemaMigrator;

            Logger = NullLogger<CustomerStoreMigrationService>.Instance;
        }

        public ILogger<CustomerStoreMigrationService> Logger { get; set; }

        public async Task MigrateAsync()
        {
            Logger.LogInformation("Started database migrations...");

            Logger.LogInformation("Migrating database schema...");
            await _dbSchemaMigrator.MigrateAsync();

            Logger.LogInformation("Successfully completed database migrations.");
        }
    }
}