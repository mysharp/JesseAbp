using Galaxy.Product.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Galaxy.Order.Api
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule))]
    [DependsOn(typeof(AbpAutofacModule))]
    [DependsOn(typeof(AbpHttpClientModule))]    //引入此Module，不然HttpClientProxy报错
    [DependsOn(typeof(GalaxyOrderModule))]
    [DependsOn(typeof(GalaxyProductContractModule))]
    public class GalaxyOrderApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //创建动态客户端代理
            context.Services.AddHttpClientProxies(
                typeof(GalaxyProductContractModule).Assembly, remoteServiceConfigurationName: "ProductStore"
            );

            Configure<AbpAspNetCoreMvcOptions>(options =>
                {
                    options.ConventionalControllers.Create(typeof(GalaxyOrderModule).Assembly);
                });

            ConfigureSwaggerServices(context.Services);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            app.UseRouting();
            app.UseMvcWithDefaultRouteAndArea();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Galaxy Order API(MicroServices)");
            });
        }

        private void ConfigureSwaggerServices(IServiceCollection service)
        {
            service.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",new OpenApiInfo{Title = "Galaxy Order API(MicroServices)", Version = "v0.1"});
                options.DocInclusionPredicate((doc, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
        }
    }
}