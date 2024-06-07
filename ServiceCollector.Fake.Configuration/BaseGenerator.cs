using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace ServiceCollector.Fake.Configuration;

public static class BaseGenerator
{
    private static readonly IFixture Fixture;
    static BaseGenerator()
    {
        Fixture = new Fixture().Customize(new AutoNSubstituteCustomization()
        {
            ConfigureMembers = true
        });
    }

    public static TModel Create<TModel>()
    {
        return Fixture.Create<TModel>();
    }
}