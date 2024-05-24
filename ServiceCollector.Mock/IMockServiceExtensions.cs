using ServiceCollector.Abstractions;

namespace ServiceCollector.Mock;

public static class MockServiceExtensions
{
    public static TServiceType? GetService<TServiceType>(this IMockService mockService) where TServiceType : class
    {
        var serviceDescriptor =
            mockService.MockedServiceCollection.FirstOrDefault(x => x.ServiceType == typeof(TServiceType));
        return serviceDescriptor?.ImplementationInstance as TServiceType;
    }

    public static IList<TServiceType?> GetServices<TServiceType>(this IMockService mockService)
        where TServiceType : class
    {
        var serviceDescriptor = mockService.MockedServiceCollection.Where(x => x.ServiceType == typeof(TServiceType));
        return serviceDescriptor.Select(x => x.ImplementationInstance as TServiceType).ToList();
    }
}