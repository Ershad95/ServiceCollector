﻿using System;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceCollector.Abstractions;

namespace ServiceCollector.Fake;

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
        Action<ServiceConfigExtension.FakeConfiguration<TService>>? action = null,
        string currentEnvironment = "Development",
        string targetEnvironment = "Development")
        where TService : class
    {
        var obj = AutoFixture.Create<TService>();
        var fakeConfiguration = new FakeConfiguration<TService>(obj);
        action?.Invoke(fakeConfiguration);

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
        var fakeConfiguration = new FakeConfigurationWithMultiEnvironment<TService>(currentEnvironment);
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
}