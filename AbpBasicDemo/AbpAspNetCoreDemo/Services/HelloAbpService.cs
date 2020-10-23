using System;
using Volo.Abp.DependencyInjection;

namespace AbpAspNetCoreDemo.Services
{
    public interface IHelloAbpService
    {
        string GetHelloString();
    }

    public class HelloAbpService :ITransientDependency, IHelloAbpService
    {
        public string GetHelloString()
        {
            return "Hello, Abp!";
        }
    }
}