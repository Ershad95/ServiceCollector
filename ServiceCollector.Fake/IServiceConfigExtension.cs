using System;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Dsl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using ServiceCollector.Abstractions;

namespace ServiceCollector.Fake
{
    public static partial class ServiceConfigExtension
    {
        private static readonly IFixture AutoFixture;

        static ServiceConfigExtension()
        {
            AutoFixture = new Fixture()
                .Customize(new AutoNSubstituteCustomization()
                {
                    ConfigureMembers = true
                });
        }

        public static IServiceConfig Fake<TService>(
            this IServiceConfig serviceConfig,
            Action<FakeConfiguration<TService>>? action = null,
            string currentEnvironment = "Development",
            string targetEnvironment = "Development")
            where TService : class
        {
            if (action is null)
                return serviceConfig;

            var fakeConfiguration = new FakeConfiguration<TService>(AutoFixture.Create<TService>());
            action(fakeConfiguration);

            if (string.Equals(currentEnvironment, targetEnvironment, StringComparison.InvariantCultureIgnoreCase))
            {
                serviceConfig
                    .ServiceCollection
                    .Replace(new ServiceDescriptor(typeof(TService), fakeConfiguration.Service));
            }

            return serviceConfig;
        }

        public static IServiceConfig FakeInMultiEnvironments<TService>(
            this IServiceConfig serviceConfig,
            Action<FakeConfigurationWithMultiEnvironment<TService>> action,
            string currentEnvironment = "Development")
            where TService : class
        {
            var fakeConfiguration = new FakeConfigurationWithMultiEnvironment<TService>();
            action(fakeConfiguration);

            var service = fakeConfiguration.Services[currentEnvironment];
            if (service != null)
            {
                serviceConfig
                    .ServiceCollection
                    .Replace(new ServiceDescriptor(typeof(TService), service));
            }

            return serviceConfig;
        }

        public static void ResultBuilder<TObject>(
            this TObject result,
            Action<TObject> action)
            where TObject : class
        {
            action(result);
        }
    }
}