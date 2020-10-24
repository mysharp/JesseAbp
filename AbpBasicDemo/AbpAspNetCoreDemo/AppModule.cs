using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AbpAspNetCoreDemo
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule))]
    [DependsOn(typeof(AbpAutofacModule))]
    public class AppModule: AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            Console.WriteLine("PreConfigureServices");

            base.PreConfigureServices(context);
        }
        
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Console.WriteLine("ConfigureServices");

            base.ConfigureServices(context);
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            Console.WriteLine("PostConfigureServices");

            base.PostConfigureServices(context);
        }

        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            Console.WriteLine("OnPreApplicationInitialization");

            base.OnPreApplicationInitialization(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            Console.WriteLine("OnApplicationInitialization");

            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseConfiguredEndpoints();
        }

        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            Console.WriteLine("OnPostApplicationInitialization");

            base.OnPostApplicationInitialization(context);
        }
    }
}