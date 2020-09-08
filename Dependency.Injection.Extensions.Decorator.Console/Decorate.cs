using System;
using Microsoft.Extensions.DependencyInjection;

namespace Dependency.Injection.Extensions.Decorator
{
    public static class Decorate
    {
        public static Func<IServiceProvider, TServiceType> WithInnerType<TServiceType, TInnerType>()
        {
            return p => (TServiceType)ActivatorUtilities.CreateInstance(p, typeof(TServiceType), p.GetRequiredService<TInnerType>());
        }
    }
}