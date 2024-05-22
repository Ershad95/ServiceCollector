using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ServiceCollector.Abstractions;

namespace ServiceCollector.Validation;

public static class ServiceCollectionExtensions
{
    private static readonly ServiceValidator ServiceValidator;
    private static IServiceCollection _serviceCollection;
    private static ICollection<Assembly> _assemblies;

    static ServiceCollectionExtensions()
    {
        ServiceValidator = new ServiceValidator()
        {
            StartNames = Array.Empty<string>(),
            EndNames = Array.Empty<string>()
        };
        _serviceCollection = new ServiceCollection();
        _assemblies = new List<Assembly>();
    }

    public static IServiceValidator ValidationSetting(this IServiceConfig serviceConfig)
    {
        _serviceCollection = serviceConfig.ServiceCollection;
        _assemblies = serviceConfig.SelectedAssemblies;
        return ServiceValidator;
    }

    public static IServiceValidator WithException(this IServiceValidator serviceValidator, Exception exception)
    {
        ServiceValidator.Exception = exception;
        return ServiceValidator;
    }

    public static IServiceValidator WithStartName(this IServiceValidator serviceValidator, params string[] startNames)
    {
        ServiceValidator.StartNames = startNames;
        return ServiceValidator;
    }

    public static IServiceValidator WithEndName(this IServiceValidator serviceValidator, params string[] endNames)
    {
        ServiceValidator.EndNames = endNames;
        return ServiceValidator;
    }

    public static void Validate(this IServiceValidator serviceValidator)
    {
        var typeInfos = _assemblies.SelectMany(x => x.DefinedTypes)
            .Where(typeInfo => typeInfo.IsClass &&
                               !typeInfo.FullName!.StartsWith("Microsoft.") &&
                               !typeInfo.Namespace!.StartsWith("Microsoft.") &&
                               !typeInfo.Name!.StartsWith("Microsoft.") &&
                               typeInfo.CustomAttributes.All(attributeData =>
                                   attributeData.AttributeType != typeof(IgnoreValidationAttribute)));


        IList<TypeInfo> filteredTypeInfos = new List<TypeInfo>();


        foreach (var startName in ServiceValidator.StartNames)
        {
            foreach (var typeInfo in typeInfos)
            {
                if (typeInfo.Name.StartsWith(startName))
                {
                    filteredTypeInfos.Add(typeInfo);
                }
            }
        }

        foreach (var endName in ServiceValidator.EndNames)
        {
            foreach (var typeInfo in typeInfos)
            {
                if (typeInfo.Name.EndsWith(endName))
                {
                    filteredTypeInfos.Add(typeInfo);
                }
            }
        }

        if (filteredTypeInfos.Any())
        {
            foreach (var filteredTypeInfo in filteredTypeInfos)
            {
                if (_serviceCollection.Any(descriptor =>
                        descriptor.ImplementationType != null &&
                        descriptor.ImplementationType.Equals(filteredTypeInfo)))
                    continue;

                if (_serviceCollection.Any(descriptor => descriptor.ServiceType.Equals(filteredTypeInfo)))
                    continue;

                if (serviceValidator.Exception is not null)
                {
                    throw serviceValidator.Exception;
                }

                throw new Exception($"there is not any registered for {filteredTypeInfo.FullName}");
            }

            return;
        }

        if (serviceValidator.Exception is not null)
        {
            throw serviceValidator.Exception;
        }

        throw new Exception("there is not any service according to conditions");
    }
}