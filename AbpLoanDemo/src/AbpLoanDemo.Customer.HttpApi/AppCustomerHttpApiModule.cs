using AbpLoanDemo.Customer.Application;
using AbpLoanDemo.Customer.Application.DomainEventHandlers;
using AbpLoanDemo.Customer.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Threading.Tasks;
using EasyAbp.Abp.EventBus.Cap;
using EasyAbp.Abp.EventBus.CAP.SqlServer;
using Microsoft.Extensions.Configuration;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;

namespace AbpLoanDemo.Customer.HttpApi
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule), typeof(AbpAutofacModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule))]
    [DependsOn(typeof(AbpEventBusCapModule), typeof(AbpEventBusCapSqlServerModule))]
    [DependsOn(typeof(AppCustomerApplicationModule))]
    [DependsOn(typeof(AppCustomerEntityFrameworkCoreModule))]
    public class AppCustomerHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(AppCustomerApplicationModule).Assembly);
            });

            context.Services.AddMediatR(typeof(CustomerLinkmanAddedDomainEventHandler));

            var configuration = context.Services.GetConfiguration();

            ConfigureCapEventBus(context, configuration);

            ConfigureAuthentication(context.Services, configuration);

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
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API"); });
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
                capOptions.UseEntityFramework<CustomerDbContext>();
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
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "Customer API", Version = "v0.1"});
                options.DocInclusionPredicate((doc, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
        }
    }
}