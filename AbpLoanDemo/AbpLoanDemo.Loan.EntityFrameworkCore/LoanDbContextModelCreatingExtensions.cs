using AbpLoanDemo.Loan.Domain.Entities;
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

            builder.Entity<LoanRequest>(c =>
            {
                c.ToTable("LoanRequest");
                c.ConfigureByConvention();

                c.Property(p => p.Score).HasColumnType("decimal(18,2)").HasDefaultValue(0);
                c.Property(p => p.Amount).HasColumnType("decimal(18,2)").HasDefaultValue(0);

                c.Metadata.FindNavigation(nameof(LoanRequest.Partners))
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
            });

            builder.Entity<Applier>(c =>
            {
                c.ToTable("Applier");
                c.ConfigureByConvention();

                c.Property(p => p.Name).HasMaxLength(100).IsRequired();
                c.Property(p => p.Phone).HasMaxLength(50).IsRequired(false);
                c.Property(p => p.IdNo).HasMaxLength(50).IsRequired(false);
            });

            builder.Entity<Guarantee>(c =>
            {
                c.ToTable("Guarantee");
                c.ConfigureByConvention();

                c.Property(p => p.Name).HasMaxLength(100).IsRequired();
                c.Property(p => p.Cost).HasColumnType("decimal(18,2)").HasDefaultValue(0);
                c.Property(p => p.ExpiryDate).HasColumnType("date").IsRequired(false);
            });
        }
    }
}