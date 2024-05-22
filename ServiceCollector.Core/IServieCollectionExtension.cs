using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ServiceCollector.Abstractions;

namespace ServiceCollector.Core;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// get all classes that Implemented IServiceDiscovery and call add service for collect all services.
    /// </summary>
    /// <param name="assemblies"></param>
    /// <param name="serviceCollection"></param>
    /// <exception cref="InvalidOperationException"></exception>
    private static void ServiceDiscovery(IEnumerable<Assembly> assemblies, IServiceCollection serviceCollection)
    {
        const string collectorMethod = nameof(IServiceDiscovery.AddServices);
        var indicator = typeof(IServiceDiscovery);

        try
        {
            var types = assemblies.SelectMany(x => x.DefinedTypes);
            var serviceDiscoveries = types.Where(typeInfo => typeInfo.ImplementedInterfaces.Contains(indicator));

            foreach (var typeInfo in serviceDiscoveries)
            {
                var serviceInstance = Activator.CreateInstance(typeInfo);
                var methodInfo = typeInfo.GetMethod(collectorMethod);
                methodInfo?.Invoke(serviceInstance, parameters: new object?[] { serviceCollection });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new InvalidOperationException(e.Message);
        }
    }

    public static void AddServiceDiscovery(this IServiceCollection serviceCollection)
    {
        ServiceDiscovery(AppDomain.CurrentDomain.GetAssemblies(), serviceCollection);
    }

    public static void AddServiceDiscovery(this IServiceCollection serviceCollection, Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(assemblies);

        ServiceDiscovery(assemblies.Length > 0 ? assemblies : AppDomain.CurrentDomain.GetAssemblies(), serviceCollection);
    }
}