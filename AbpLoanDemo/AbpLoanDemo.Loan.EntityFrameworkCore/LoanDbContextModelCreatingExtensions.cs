using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace AbpLoanDemo.Loan.EntityFrameworkCore
{
    public static class LoanDbContextModelCreatingExtensions
    {
        public static void ConfigureLoanStore(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Domain.Entities.LoanRequest>(c =>
            {
                c.ToTable("LoanRequest");
                c.ConfigureByConvention();
            });

            builder.Entity<Domain.Entities.Applier>(c =>
            {
                c.ToTable("Applier");
                c.ConfigureByConvention();
            });

            builder.Entity<Domain.Entities.Guarantee>(c =>
            {
                c.ToTable("Guarantee");
                c.ConfigureByConvention();
            });
        }
    }
}