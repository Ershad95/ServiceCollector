using Microsoft.Extensions.DependencyInjection;
using ServiceCollector.Abstractions;

namespace ServiceCollector.Mock;


public class MockService : IMockService
{
    public MockService(IServiceCollection mockedServiceCollection)
    {
        MockedServiceCollection = mockedServiceCollection;
    }
    public IServiceCollection MockedServiceCollection { get; }
}
