using System;
using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace ServiceCollector.Fake
{
    public static class ObjectBuilderExtension
    {
        private static readonly IFixture AutoFixture;
        static ObjectBuilderExtension()
        {
            AutoFixture = new Fixture()
                .Customize(new AutoNSubstituteCustomization()
                {
                    ConfigureMembers = true
                });
        }
        public static void ResultBuilder<TResult>(
            this TResult? result,
            Action<TResult> action)
            where TResult : class
        {
            result ??= AutoFixture.Create<TResult>();
            action(result);
        }
        
        public static void ResultBuilder<TResult>(
            this TResult result,
            TResult value)
            where TResult : struct
        {
            result = value;
        }
    }
}