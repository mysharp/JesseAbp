using Volo.Abp.Modularity;

namespace AbpLoanDemo.Identity
{
    [DependsOn(
        typeof(IdentityApplicationModule),
        typeof(IdentityDomainTestModule)
        )]
    public class IdentityApplicationTestModule : AbpModule
    {

    }
}