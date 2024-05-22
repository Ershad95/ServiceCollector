using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceCollector.Abstractions;

public interface IServiceConfig
{
    ICollection<Assembly> SelectedAssemblies { get; }
    IServiceCollection ServiceCollection { get; }
}