using Microsoft.Extensions.DependencyInjection;

namespace ServiceCollector.Abstractions;

public interface IMockService
{
    public IServiceCollection MockedServiceCollection { get; }
}