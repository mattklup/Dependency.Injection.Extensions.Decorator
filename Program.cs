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

            bool useOption1 = false;

            if (useOption1)
            {
                services.AddSingleton<ISampleService>(
                        Decorate<ISampleService>
                            .This<SampleService>()
                            .With<CachingSampleService>()
                            .With<LoggingSampleService>()
                            .Factory()
                    );
            }
            else
            {
                services.AddSingleton<SampleService>();
                services.AddSingleton<CachingSampleService>(Decorate.WithInnerType<CachingSampleService, SampleService>());
                services.AddSingleton<ISampleService>(Decorate.WithInnerType<LoggingSampleService, CachingSampleService>());
            }
        }

        static void Run(IServiceProvider serviceProvider)
        {
            var log = serviceProvider.GetRequiredService<TextWriter>();
            var service = serviceProvider.GetRequiredService<ISampleService>();

            log.WriteLine("First call");
            service.Process(5);

            log.WriteLine();
            log.WriteLine("Second call");
            service.Process(5);
        }
    }
}
