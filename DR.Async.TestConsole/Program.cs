
using System;
using System.Diagnostics;
using DR.Async.Tasks.Handling_Exceptions;
using DR.Async.Tasks.ThreadVsTask;
using Demonstration = DR.Async.Demo.Infrastructure;

namespace DR.Async.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Demonstration.Demo.Start<ThreadVsTaskSpeed>((demo) =>
            {
                demo.StartThreads();
                demo.StartTasks();
            });*/

            //Demonstration.Demo.Start<HandlingUsingExceptionHandle>();
            Demonstration.Demo.Start<HandlingExceptionsUsingTryCatch>();
        }
    }
}
