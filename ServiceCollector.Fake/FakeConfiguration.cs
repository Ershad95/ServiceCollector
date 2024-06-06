namespace ServiceCollector.Fake
{
    public static partial class ServiceConfigExtension
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
}
