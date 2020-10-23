using System;

namespace AbpConsoleDemo
{
    public interface IHelloAbpService
    {
        void SayHello();
    }

    public class HelloAbpService : IHelloAbpService
    {
        public void SayHello()
        {
            Console.WriteLine("Hello, Abp!");
        }
    }
}