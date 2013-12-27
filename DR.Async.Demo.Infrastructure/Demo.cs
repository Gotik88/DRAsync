using System;
using DR.Async.Task.Helpers;

namespace DR.Async.Demo.Infrastructure
{
    public static class Demo
    {
        public static void Start<T>(Action<T> steps = null) where T : IDisposable, new()
        {
            using (T instance = new T())
            {
                var currentExample = instance;
                steps.Do(a => a(currentExample));
            }
        }
    }
}
