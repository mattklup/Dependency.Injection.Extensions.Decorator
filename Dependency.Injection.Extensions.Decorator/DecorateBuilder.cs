using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Dependency.Injection.Extensions.Decorator
{
    public class Decorate<T>
    {
        private readonly List<Func<IServiceProvider, T>> factories = new List<Func<IServiceProvider, T>>();

        private Decorate(Type rootType)
        {
            this.factories.Add(
                p => (T) ActivatorUtilities.CreateInstance(p, rootType)
            );
        }

        public static Decorate<T> This<TImplementation>()
        {
            return new Decorate<T>(typeof(TImplementation));
        }

        public Decorate<T> With<TWrapper>()
        {
            var innerFactory = this.factories.Last();

            this.factories.Add(
                p => (T) ActivatorUtilities.CreateInstance(p, typeof(TWrapper), innerFactory(p))
            );

            return this;
        }

        public Func<IServiceProvider, T> Factory()
        {
            return this.factories.Last();
        }
    }

}