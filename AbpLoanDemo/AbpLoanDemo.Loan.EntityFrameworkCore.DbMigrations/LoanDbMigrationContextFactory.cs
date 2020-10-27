using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AbpLoanDemo.Loan.EntityFrameworkCore.DbMigrations
{
    public class LoanDbMigrationContextFactory : IDesignTimeDbContextFactory<LoanDbMigrationContext>
    {
        public LoanDbMigrationContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<LoanDbMigrationContext>()
                .UseSqlServer("LoanConnString");

            return new LoanDbMigrationContext(builder.Options);
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