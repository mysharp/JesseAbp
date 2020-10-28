using System.Threading.Tasks;
using AbpLoanDemo.Customer.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace AbpLoanDemo.Customer.EntityFrameworkCore.DbMigrations
{
    [Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
    [ExposeServices(typeof(ICustomerStoreSchemaMigrator))]
    public class EntityFrameworkCoreCustomerDbSchemaMigrator : ICustomerStoreSchemaMigrator
    {
        private readonly CustomerDbMigrationContext _dbContext;

        public EntityFrameworkCoreCustomerDbSchemaMigrator(CustomerDbMigrationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task MigrateAsync()
        {
            await _dbContext.Database.MigrateAsync();
        }
    }
}