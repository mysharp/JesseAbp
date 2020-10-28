using AbpLoanDemo.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace AbpLoanDemo.Identity
{
    [DependsOn(
        typeof(IdentityEntityFrameworkCoreTestModule)
        )]
    public class IdentityDomainTestModule : AbpModule
    {

    }
}