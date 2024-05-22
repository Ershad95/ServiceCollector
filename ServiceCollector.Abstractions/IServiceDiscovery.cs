using Microsoft.Extensions.DependencyInjection;

namespace ServiceCollector.Abstractions;

public interface IServiceDiscovery
{
    void AddServices(IServiceCollection serviceCollection);
}
