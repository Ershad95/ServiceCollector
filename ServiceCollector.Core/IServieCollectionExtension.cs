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

    /// <summary>
    /// add all services that exist in all assemblies
    /// </summary>
    /// <param name="serviceCollection"></param>
    public static IServiceConfig AddServiceDiscovery(this IServiceCollection serviceCollection)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        ServiceDiscovery(assemblies, serviceCollection);

        return new ServiceConfig(assemblies,serviceCollection);
    }

    /// <summary>
    /// add services that exist in selected assemblies if list of assemblies are empty collect services that exist in all assemblies
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="selectedAssemblies"></param>
    public static IServiceConfig AddServiceDiscovery(this IServiceCollection serviceCollection, Assembly[] selectedAssemblies)
    {
        ArgumentNullException.ThrowIfNull(selectedAssemblies);

        var assemblies = selectedAssemblies.Length > 0 ? selectedAssemblies : AppDomain.CurrentDomain.GetAssemblies();
        ServiceDiscovery(assemblies, serviceCollection);
        
        return new ServiceConfig(assemblies,serviceCollection);

    }
}