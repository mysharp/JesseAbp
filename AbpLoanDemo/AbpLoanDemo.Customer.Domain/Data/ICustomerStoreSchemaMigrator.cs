using System.Threading.Tasks;

namespace AbpLoanDemo.Customer.Domain.Data
{
    public interface ICustomerStoreSchemaMigrator
    {
        Task MigrateAsync();
    }
}