using System;

namespace ServiceCollector.Fake
{
    public static class ObjectBuilderExtension
    {
        public static void ResultBuilder<TResult>(
            this TResult result,
            Action<TResult> action)
            where TResult : class
        {
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