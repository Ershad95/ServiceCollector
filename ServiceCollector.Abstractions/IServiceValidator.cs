using System;

namespace ServiceCollector.Abstractions
{
    public interface IServiceValidator
    {
        Exception? Exception { get; }
        string[] StartNames { get; }
        string[] EndNames { get; }
    }
}

