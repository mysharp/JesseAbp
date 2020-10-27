using System.Threading.Tasks;

namespace AbpLoanDemo.Loan.Domain.Data
{
    public interface ILoanStoreSchemaMigrator
    {
        Task MigrateAsync();
    }
}