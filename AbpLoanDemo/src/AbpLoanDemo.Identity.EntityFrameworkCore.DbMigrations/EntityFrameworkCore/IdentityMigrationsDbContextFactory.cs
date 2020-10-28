using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AbpLoanDemo.Identity.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class IdentityMigrationsDbContextFactory : IDesignTimeDbContextFactory<IdentityMigrationsDbContext>
    {
        public IdentityMigrationsDbContext CreateDbContext(string[] args)
        {
            IdentityEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<IdentityMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new IdentityMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../AbpLoanDemo.Identity.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
