using AbpLoanDemo.Customer.Application;
using AbpLoanDemo.Customer.Application.DomainEventHandlers;
using AbpLoanDemo.Customer.EntityFrameworkCore;
using AbpLoanDemo.Loan.Application;
using AbpLoanDemo.Loan.Application.DomainEventHandlers;
using AbpLoanDemo.Loan.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AbpLoanDemo.HttpApi
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule))]
    [DependsOn(typeof(AbpAutofacModule))]
    [DependsOn(typeof(AppLoanApplicationModule))]
    [DependsOn(typeof(AppCustomerApplicationModule))]
    [DependsOn(typeof(AppLoanEntityFrameworkCoreModule))]
    [DependsOn(typeof(AppCustomerEntityFrameworkCoreModule))]
    public class AppHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(AppCustomerApplicationModule).Assembly);
                options.ConventionalControllers.Create(typeof(AppLoanApplicationModule).Assembly);
            });

            context.Services.AddMediatR(typeof(CustomerChangedDomainEventHandler),
                typeof(LoanRequestAddedDomainEventHandler));

            ConfigureSwaggerServices(context.Services);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            app.UseRouting();
            app.UseConfiguredEndpoints();

            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Loan Monolith API"); });
        }

        private void ConfigureSwaggerServices(IServiceCollection service)
        {
            service.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "Loan Monolith API", Version = "v0.1"});
                options.DocInclusionPredicate((doc, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
        }
    }
}