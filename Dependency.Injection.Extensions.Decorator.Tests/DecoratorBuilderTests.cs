using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Dependency.Injection.Extensions.Decorator.Tests
{
    public class DecoratorBuilderTests
    {
        private readonly ServiceCollection services = new ServiceCollection();

        [Fact]
        public void BasicTest()
        {
            services.AddSingleton<ITest>(
                        Decorate<ITest>
                            .This<Test>()
                            .Factory()
                    );

            var provider = services.BuildServiceProvider();

            ITest test = provider.GetRequiredService<ITest>();

            Assert.Equal(5, test.GetNumber());
        }


        [Fact]
        public void BasicExtensionTest()
        {
            services.AddDecoratedSingleton(
                        Decorate<ITest>
                            .This<Test>()
                    );

            var provider = services.BuildServiceProvider();

            ITest test = provider.GetRequiredService<ITest>();

            Assert.Equal(5, test.GetNumber());
        }

        [Fact]
        public void BasicExtension2Test()
        {
            services.AddDecoratedSingleton<ITest, Test>(
                        (d) => d.With<TestVerifier>()
                    );

            var provider = services.BuildServiceProvider();

            ITest test = provider.GetRequiredService<ITest>();

            Assert.Equal(5, test.GetNumber());
        }

        [Fact]
        public void BasicDecorateTest()
        {
            services.AddSingleton<ICallCount, CallCounter>();
            services.AddSingleton<ITest>(
                        Decorate<ITest>
                            .This<Test>()
                            .With<TestVerifier>()
                            .Factory()
                    );

            var provider = services.BuildServiceProvider();

            ITest test = provider.GetRequiredService<ITest>();

            Assert.Equal(5, test.GetNumber());
            Assert.Equal(5, test.GetNumber());
            Assert.Equal(5, test.GetNumber());

            Assert.Equal(
                3,
                provider.GetRequiredService<ICallCount>().CallCount);
        }

        interface ITest
        {
            int GetNumber();
        }

        interface ICallCount
        {
            int CallCount { get; }

            void Increment();
        }

        class CallCounter : ICallCount
        {
            public int CallCount { get; private set; }

            public CallCounter()
            {
            }

            public void Increment() => ++this.CallCount;
        }

        class TestVerifier : ITest
        {
            private readonly ICallCount count;
            private readonly ITest test;

            public TestVerifier(ITest innerTest, ICallCount callCount)
            {
                this.count = callCount;
                this.test = innerTest;
            }

            public int GetNumber()
            {
                this.count.Increment();
                return this.test.GetNumber();
            }
        }



        class Test : ITest
        {
            public int GetNumber() => 5;
        }
    }
}
