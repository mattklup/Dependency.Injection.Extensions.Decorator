using System;
using Microsoft.Extensions.DependencyInjection;

namespace Dependency.Injection.Extensions.Decorator
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceCollection AddDecoratedSingleton<TService>(this ServiceCollection services, Decorate<TService> builder) where TService : class
        {
            services.AddSingleton<TService>(builder.Factory());

            return services;
        }

        public static ServiceCollection AddDecoratedSingleton<TInterface, TService>(
            this ServiceCollection services,
            Func<Decorate<TInterface>, Decorate<TInterface>> decorator) where TInterface : class
        {
            services.AddSingleton<TInterface>(decorator(Decorate<TInterface>.This<TService>()).Factory());

            return services;
        }
    }
}