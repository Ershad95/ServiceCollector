using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ServiceCollector.Abstractions;

namespace ServiceCollector.Core;

public static class ServiceCollectionExtension
{
    private static void Discovery(IEnumerable<Assembly> assemblies,IServiceCollection serviceCollection)
    {
        var types = assemblies.SelectMany(x => x.DefinedTypes);
        var serviceDiscoveries =
            types.Where(typeInfo => typeInfo.ImplementedInterfaces.Contains(typeof(IServiceDiscovery)));

        foreach (var typeInfo in serviceDiscoveries)
        {
            var instance = Activator.CreateInstance(typeInfo);
            var method = typeInfo.GetMethod(nameof(IServiceDiscovery.Add));
            method?.Invoke(instance, parameters: new object?[] { serviceCollection });
        }
    }
    public static void AddServiceDiscovery(this IServiceCollection serviceCollection)
    {
        try
        {
            Discovery(AppDomain.CurrentDomain.GetAssemblies(), serviceCollection);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(e.Message);
        }
    }

    public static void AddServiceDiscovery(this IServiceCollection serviceCollection, Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(assemblies);
        
        try
        {
            Discovery(assemblies.Length > 0 ? assemblies : AppDomain.CurrentDomain.GetAssemblies(),serviceCollection);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(e.Message);
        }
    }
}