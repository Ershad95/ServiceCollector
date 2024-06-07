namespace ServiceCollector.Fake.Configuration
{
    public class FakeConfigurationWithMultiEnvironment<TService>
        where TService : class
    {
        private readonly string _currentEnv;
        public IDictionary<string, TService> Services { get; private set; }

        public FakeConfigurationWithMultiEnvironment(string currentEnv)
        {
            _currentEnv = currentEnv;
            Services = new Dictionary<string, TService>();
        }

        public void Add(string targetEnvironment, Action<TService> service)
        {
            var serviceObject = BaseGenerator.Create<TService>();
            service(serviceObject);
            Services.Add(targetEnvironment, serviceObject);
        }

        /// <summary>
        /// set current env as target env
        /// </summary>
        /// <param name="service"></param>
        public void Add(Action<TService> service)
        {
            var serviceObject = BaseGenerator.Create<TService>();
            service(serviceObject);
            Services.Add(_currentEnv, serviceObject);
        }
    }
}