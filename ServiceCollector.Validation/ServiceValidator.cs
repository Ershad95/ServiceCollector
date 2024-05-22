using ServiceCollector.Abstractions;

namespace ServiceCollector.Validation;

public class ServiceValidator : IServiceValidator
{
    public Exception? Exception { get; set; }
    public string[] StartNames { get; set; }
    public string[] EndNames { get; set; }
}