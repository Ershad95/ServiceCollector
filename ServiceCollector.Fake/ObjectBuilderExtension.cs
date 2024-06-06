using System;

namespace ServiceCollector.Fake
{
    public static class ObjectBuilderExtension
    {
        public static void ResultBuilder<TObject>(
            this TObject result,
            Action<TObject> action)
            where TObject : class
        {
            action(result);
        }
    }
}