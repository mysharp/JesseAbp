using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace AbpLoanDemo.Identity.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(IdentityHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class IdentityConsoleApiClientModule : AbpModule
    {
        
    }
}
