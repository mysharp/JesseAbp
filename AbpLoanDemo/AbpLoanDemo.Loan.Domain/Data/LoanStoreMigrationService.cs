using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.DependencyInjection;

namespace AbpLoanDemo.Loan.Domain.Data
{
    public class LoanStoreMigrationService : ITransientDependency
    {
        public ILogger<LoanStoreMigrationService> Logger { get; set; }

        private readonly ILoanStoreSchemaMigrator _dbSchemaMigrator;

        public LoanStoreMigrationService(ILoanStoreSchemaMigrator dbSchemaMigrator)
        {
            _dbSchemaMigrator = dbSchemaMigrator;

            Logger = NullLogger<LoanStoreMigrationService>.Instance;
        }

        public async Task MigrateAsync()
        {
            Logger.LogInformation("Started database migrations...");

            Logger.LogInformation("Migrating database schema...");
            await _dbSchemaMigrator.MigrateAsync();

            Logger.LogInformation("Successfully completed database migrations.");
        }

    }
}