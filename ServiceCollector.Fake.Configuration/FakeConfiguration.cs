namespace ServiceCollector.Fake.Configuration
{
    public class FakeConfiguration<TService> where TService : class
    {
        public TService Service { get; }

        public FakeConfiguration(TService service)
        {
            Service = service;
        }
    }
}
