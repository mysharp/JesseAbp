using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.DependencyInjection;

namespace AbpLoanDemo.Loan.Domain.Data
{
    public class LoanStoreMigrationService : ITransientDependency
    {
        private readonly ILoanStoreSchemaMigrator _dbSchemaMigrator;

        public LoanStoreMigrationService(ILoanStoreSchemaMigrator dbSchemaMigrator)
        {
            _dbSchemaMigrator = dbSchemaMigrator;

            Logger = NullLogger<LoanStoreMigrationService>.Instance;
        }

        public ILogger<LoanStoreMigrationService> Logger { get; set; }

        public async Task MigrateAsync()
        {
            Logger.LogInformation("Started Loan database migrations...");

            Logger.LogInformation("Migrating Loan database schema...");
            await _dbSchemaMigrator.MigrateAsync();

            Logger.LogInformation("Successfully completed Loan database migrations.");
        }
    }
}