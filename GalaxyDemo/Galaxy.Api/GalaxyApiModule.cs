﻿using Galaxy.Order;
using Galaxy.Product;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Galaxy.Api
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule))]
    [DependsOn(typeof(AbpAutofacModule))]
    [DependsOn(typeof(GalaxyOrderModule))]
    [DependsOn(typeof(GalaxyProductModule))]
    public class GalaxyApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
                {
                    options.ConventionalControllers.Create(typeof(GalaxyOrderModule).Assembly);
                    options.ConventionalControllers.Create(typeof(GalaxyProductModule).Assembly);
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
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Galaxy Monolith API");
            });
        }

        private void ConfigureSwaggerServices(IServiceCollection service)
        {
            service.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",new OpenApiInfo{Title = "Galaxy Monolith API", Version = "v0.1"});
                options.DocInclusionPredicate((doc, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
        }
    }
}