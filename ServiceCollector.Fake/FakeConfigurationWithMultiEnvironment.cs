using System;
using System.Collections.Generic;
using AutoFixture;

namespace ServiceCollector.Fake
{
    
public static partial class ServiceConfigExtension
{
    public class FakeConfigurationWithMultiEnvironment<TService> where TService : class
    {
        private readonly string _currentEnv;
        internal IDictionary<string, TService> Services { get; private set; }

        public FakeConfigurationWithMultiEnvironment(string currentEnv)
        {
            _currentEnv = currentEnv;
            Services = new Dictionary<string, TService>();
        }

        public void Add(string targetEnvironment, Action<TService> service)
        {
            var obj = AutoFixture.Create<TService>();
            service(obj);
            Services.Add(targetEnvironment, obj);
        }
        /// <summary>
        /// set current env as target env
        /// </summary>
        /// <param name="service"></param>
        public void Add(Action<TService> service)
        {
            var obj = AutoFixture.Create<TService>();
            service(obj);
            Services.Add(_currentEnv, obj);
        }
    }
}
}