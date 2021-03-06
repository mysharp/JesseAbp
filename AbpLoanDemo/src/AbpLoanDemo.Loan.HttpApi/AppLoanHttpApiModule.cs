﻿using AbpLoanDemo.Customer.Application.Contracts;
using AbpLoanDemo.Loan.Application;
using AbpLoanDemo.Loan.Application.DomainEventHandlers;
using AbpLoanDemo.Loan.EntityFrameworkCore;
using EasyAbp.Abp.EventBus.Cap;
using EasyAbp.Abp.EventBus.CAP.SqlServer;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;

namespace AbpLoanDemo.Loan.HttpApi
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule), typeof(AbpAutofacModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule))]
    [DependsOn(typeof(AbpEventBusCapModule),typeof(AbpEventBusCapSqlServerModule))]
    [DependsOn(typeof(AbpHttpClientModule))] //引入此Module，不然HttpClientProxy报错
    [DependsOn(typeof(AbpHttpClientIdentityModelModule))] //引入此Module，不然HttpClientProxy 不会带上Token
    [DependsOn(typeof(AppLoanApplicationModule))]
    [DependsOn(typeof(AppCustomerApplicationContractModule))]
    [DependsOn(typeof(AppLoanEntityFrameworkCoreModule))]
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

            var configuration = context.Services.GetConfiguration();

            ConfigureCapEventBus(context, configuration);
            
            ConfigureAuthentication(context.Services,configuration);

            ConfigureSwaggerServices(context.Services);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            app.UseRouting();

            app.UseJwtTokenMiddleware();
            app.UseAbpClaimsMap();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseConfiguredEndpoints();

            app.UseUnitOfWork();

            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Loan API"); });
        }

        private void ConfigureCapEventBus(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.AddCapEventBus(capOptions =>
            {
                capOptions.DefaultGroup = "AbpLoan";
                capOptions.FailedThresholdCallback = (failed) =>
                {
                    switch (failed.MessageType)
                    {
                        case DotNetCore.CAP.Messages.MessageType.Publish:
                            System.Diagnostics.Debug.WriteLine(failed.Message);
                            break;
                        case DotNetCore.CAP.Messages.MessageType.Subscribe:
                            System.Diagnostics.Debug.WriteLine(failed.Message);
                            break;
                        default:
                            break;
                    }
                };
                capOptions.UseEntityFramework<LoanDbContext>();
                capOptions.UseRabbitMQ(x =>
                {
                    x.HostName = configuration["CAP:RabbitMQ:Host"];
                    x.UserName = configuration["CAP:RabbitMQ:User"];
                    x.Password = configuration["CAP:RabbitMQ:Password"];
                    x.VirtualHost = configuration["CAP:RabbitMQ:VirtualHost"];
                });
                capOptions.UseDashboard();
            });
        }

        private void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.Audience = configuration["AuthServer:ApiName"];

                    options.RequireHttpsMetadata = false;
                    options.IncludeErrorDetails = true;

                    //options.Events.OnMessageReceived = c =>
                    //{
                    //    Console.WriteLine("Token:" + c.Token);
                    //    return Task.CompletedTask;
                    //};
                    //options.Events.OnTokenValidated = c =>
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
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "Loan API", Version = "v0.1"});
                options.DocInclusionPredicate((doc, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
        }
    }
}