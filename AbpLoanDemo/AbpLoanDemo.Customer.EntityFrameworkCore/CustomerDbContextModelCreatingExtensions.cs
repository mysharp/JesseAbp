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
            });

            builder.Entity<Domain.Entities.Linkman>(c =>
            {
                c.ToTable("Linkman");
                c.ConfigureByConvention();
            });
        }
    }
}