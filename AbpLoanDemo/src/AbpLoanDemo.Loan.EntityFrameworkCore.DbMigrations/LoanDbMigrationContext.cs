using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace AbpLoanDemo.Loan.EntityFrameworkCore.DbMigrations
{
    [ConnectionStringName("LoanConnString")]
    public class LoanDbMigrationContext : AbpDbContext<LoanDbMigrationContext>
    {
        public LoanDbMigrationContext(DbContextOptions<LoanDbMigrationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureLoanStore();
        }
    }
}