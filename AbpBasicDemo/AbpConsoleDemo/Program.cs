using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;

namespace AbpConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var application = AbpApplicationFactory.Create<AbpConsoleModule>())
            {
                application.Initialize();

                var helloAbpService = application.ServiceProvider.GetRequiredService<IHelloAbpService>();
                helloAbpService.SayHello();

                application.Shutdown();
            }


            Console.ReadLine();
        }
    }
}
