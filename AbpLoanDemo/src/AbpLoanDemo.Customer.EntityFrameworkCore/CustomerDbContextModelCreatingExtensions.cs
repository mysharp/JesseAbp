using AbpLoanDemo.Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace AbpLoanDemo.Customer.EntityFrameworkCore
{
    public static class CustomerDbContextModelCreatingExtensions
    {
        public static void ConfigureCustomerStore(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Domain.Entities.Customer>(c =>
            {
                c.ToTable("Customer");
                c.ConfigureByConvention();

                c.Property(p => p.Name).HasMaxLength(100).IsRequired();
                c.Property(p => p.Phone).HasMaxLength(50).IsRequired(false);
                c.Property(p => p.Address).IsRequired(false);
                c.Property(p => p.IdNo).HasMaxLength(50).IsRequired(false);

                c.Metadata.FindNavigation(nameof(Domain.Entities.Customer.Linkman))
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
            });

            builder.Entity<Linkman>(c =>
            {
                c.ToTable("Linkman");
                c.ConfigureByConvention();

                c.Property(p => p.Name).HasMaxLength(100).IsRequired();
                c.Property(p => p.Phone).HasMaxLength(50).IsRequired(false);
                c.Property(p => p.IdNo).HasMaxLength(50).IsRequired(false);
            });
        }
    }
}