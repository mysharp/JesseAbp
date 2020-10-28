using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace AbpLoanDemo.Identity.EntityFrameworkCore
{
    public static class IdentityDbContextModelCreatingExtensions
    {
        public static void ConfigureIdentity(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(IdentityConsts.DbTablePrefix + "YourEntities", IdentityConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}