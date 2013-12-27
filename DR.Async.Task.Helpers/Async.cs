using System;
using System.Runtime.CompilerServices;
using DR.Async.Task.Helpers.Exceptions;
using Tasks = System.Threading.Tasks;

namespace DR.Async.Task.Helpers
{
    public static class Async
    {
        public static void Call(
            this Func<Tasks.Task> asyncLambda,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            asyncLambda().HandleExceptions(filePath, lineNumber);
        }

        public static AsyncCall Prepare(this Func<Tasks.Task> asyncLambda)
        {
            return new AsyncCall(asyncLambda);
        }
    }
}
