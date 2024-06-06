using System;
using System.Collections.Generic;
using AutoFixture;

namespace ServiceCollector.Fake
{
    
public static partial class ServiceConfigExtension
{
    public class FakeConfigurationWithMultiEnvironment<TService> where TService : class
    {
        internal IDictionary<string, TService> Services { get; private set; }

        public FakeConfigurationWithMultiEnvironment()
        {
            Services = new Dictionary<string, TService>();
        }

        public void Add(string targetEnvironment, Action<TService> service)
        {
            var obj = AutoFixture.Create<TService>();
            service(obj);
            Services.Add(targetEnvironment, obj);
        }
    }
}
}