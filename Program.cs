using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace Dependency.Injection.Extensions.Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureDependencyInjection(services);

            var serviceProvider = services.BuildServiceProvider();
            Run(serviceProvider);
        }

        static void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddSingleton<TextWriter>(Console.Out);
        }

        static void Run(IServiceProvider serviceProvider)
        {
            var log = serviceProvider.GetRequiredService<TextWriter>();

            log.WriteLine("Hello World!");
        }
    }
}
