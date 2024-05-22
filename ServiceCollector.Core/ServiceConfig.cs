using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ServiceCollector.Abstractions;

namespace ServiceCollector.Core;

public class ServiceConfig : IServiceConfig
{
    public ICollection<Assembly> SelectedAssemblies { get; set; }
    public IServiceCollection ServiceCollection { get; set; }

    public ServiceConfig(ICollection<Assembly> selectedAssemblies, IServiceCollection serviceCollection)
    {
        SelectedAssemblies = selectedAssemblies;
        ServiceCollection = serviceCollection;
    }
}