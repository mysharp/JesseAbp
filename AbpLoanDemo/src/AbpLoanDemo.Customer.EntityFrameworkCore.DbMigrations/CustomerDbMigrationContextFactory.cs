using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AbpLoanDemo.Customer.EntityFrameworkCore.DbMigrations
{
    public class CustomerDbMigrationContextFactory : IDesignTimeDbContextFactory<CustomerDbMigrationContext>
    {
        public CustomerDbMigrationContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<CustomerDbMigrationContext>()
                .UseSqlServer(configuration.GetConnectionString("CustomerConnString"));

            return new CustomerDbMigrationContext(builder.Options);
        }

        private IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false);

            return builder.Build();
        }
    }
}