using System.Threading.Tasks;
using AbpLoanDemo.Loan.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace AbpLoanDemo.Loan.EntityFrameworkCore.DbMigrations
{
    [Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
    [ExposeServices(typeof(ILoanStoreSchemaMigrator))]
    public class EntityFrameworkCoreLoanDbSchemaMigrator : ILoanStoreSchemaMigrator
    {
        private readonly LoanDbMigrationContext _dbContext;

        public EntityFrameworkCoreLoanDbSchemaMigrator(LoanDbMigrationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task MigrateAsync()
        {
            await _dbContext.Database.MigrateAsync();
        }
    }
}