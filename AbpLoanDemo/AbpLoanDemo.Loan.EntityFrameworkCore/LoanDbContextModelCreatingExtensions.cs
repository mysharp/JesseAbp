using System;
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

                c.Property(p => p.Score).HasDefaultValue(0);
                c.Property(p => p.Amount).HasDefaultValue(0);

                c.Metadata.FindNavigation(nameof(Domain.Entities.LoanRequest.Partners))
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
            });

            builder.Entity<Domain.Entities.Applier>(c =>
            {
                c.ToTable("Applier");
                c.ConfigureByConvention();

                c.Property(p => p.Name).HasMaxLength(100).IsRequired();
                c.Property(p => p.Phone).HasMaxLength(50).IsRequired(false);
                c.Property(p => p.IdNo).HasMaxLength(50).IsRequired(false);
            });

            builder.Entity<Domain.Entities.Guarantee>(c =>
            {
                c.ToTable("Guarantee");
                c.ConfigureByConvention();

                c.Property(p => p.Name).HasMaxLength(100).IsRequired();
                c.Property(p => p.Cost).HasDefaultValue(0);
                c.Property(p => p.ExpiryDate).IsRequired(false);
            });
        }
    }
}