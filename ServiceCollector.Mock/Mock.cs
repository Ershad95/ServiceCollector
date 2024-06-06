using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using ServiceCollector.Abstractions;
using ServiceCollector.Core;

namespace ServiceCollector.Mock;

public static class Mock
{
    public static IMockService MockCollection(Type serviceDiscovery)
    {
        var isServiceDiscovery = serviceDiscovery.GetInterface(nameof(IServiceDiscovery));
        if (isServiceDiscovery is null)
        {
            throw new AggregateException($"input type is not {nameof(IServiceDiscovery)}");
        }
        
        var serviceCollection = new ServiceCollection();
        serviceCollection.FetchServiceCollections(serviceDiscovery);

        var services = serviceCollection.Select(x => x.ServiceType);
        foreach (var service in services)
        {
            var types = new[] { service };
            var mockedInstance = Substitute.For(types, Array.Empty<object>());
            serviceCollection.Replace(new ServiceDescriptor(service!, mockedInstance));
        }

        return new MockService(serviceCollection);
    }

    public static ICollection<IMockService> MockCollection(IEnumerable<Type> serviceDiscoveries)
    {
        return serviceDiscoveries
            .Select(MockCollection)
            .ToList();
    }
}