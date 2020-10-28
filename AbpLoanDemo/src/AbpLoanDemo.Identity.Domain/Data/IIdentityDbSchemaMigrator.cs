using System.Threading.Tasks;

namespace AbpLoanDemo.Identity.Data
{
    public interface IIdentityDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
