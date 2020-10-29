using AbpLoanDemo.Customer.Application;
using AbpLoanDemo.Customer.Application.DomainEventHandlers;
using AbpLoanDemo.Customer.EntityFrameworkCore;
using AbpLoanDemo.Loan.Application;
using AbpLoanDemo.Loan.Application.DomainEventHandlers;
using AbpLoanDemo.Loan.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
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

            ConfigureAuthentication(context.Services);

            ConfigureSwaggerServices(context.Services);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            app.UseRouting();

            app.UseAuthentication();

            app.UseJwtTokenMiddleware();
            app.UseAbpClaimsMap();

            app.UseAuthorization();

            app.UseConfiguredEndpoints();

            app.UseUnitOfWork();

            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Loan Monolith API"); });
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            var configuration = services.GetConfiguration();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.ApiName = configuration["AuthServer:ApiName"];
                    options.ApiSecret = configuration["AuthServer:ClientSecret"];

                    options.SaveToken = true;
                    options.JwtValidationClockSkew = TimeSpan.FromMinutes(30);

                    options.RequireHttpsMetadata = false;

                    //options.JwtBearerEvents.OnMessageReceived = c =>
                    //{
                    //    Console.WriteLine("Token:" + c.Token);
                    //    return Task.CompletedTask;
                    //};
                    //options.JwtBearerEvents.OnTokenValidated = c =>
                    //{
                    //    Console.WriteLine("Principal:" + c.Principal.Identity.Name);
                    //    return Task.CompletedTask;
                    //};
                });
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