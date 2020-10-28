using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace AbpLoanDemo.Customer.EntityFrameworkCore.DbMigrations
{
    [ConnectionStringName("CustomerConnString")]
    public class CustomerDbMigrationContext : AbpDbContext<CustomerDbMigrationContext>
    {
        public CustomerDbMigrationContext(DbContextOptions<CustomerDbMigrationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureCustomerStore();
        }
    }
}