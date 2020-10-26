using AbpLoanDemo.Customer.Application.Contracts;
using AbpLoanDemo.Loan.Application;
using AbpLoanDemo.Loan.Application.DomainEventHandlers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace AbpLoanDemo.Loan.HttpApi
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule))]
    [DependsOn(typeof(AbpAutofacModule))]
    [DependsOn(typeof(AbpHttpClientModule))] //引入此Module，不然HttpClientProxy报错
    [DependsOn(typeof(AppLoanApplicationModule))]
    [DependsOn(typeof(AppCustomerApplicationContractModule))]
    public class AppLoanHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //创建动态客户端代理
            context.Services.AddHttpClientProxies(
                typeof(AppCustomerApplicationContractModule).Assembly, "Customer"
            );

            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(AppLoanApplicationModule).Assembly);
            });

            context.Services.AddMediatR(typeof(LoanRequestAddedDomainEventHandler));

            ConfigureSwaggerServices(context.Services);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            app.UseRouting();
            app.UseConfiguredEndpoints();

            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Loan API"); });
        }

        private void ConfigureSwaggerServices(IServiceCollection service)
        {
            service.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "Loan API", Version = "v0.1"});
                options.DocInclusionPredicate((doc, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
        }
    }
}