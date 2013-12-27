using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DR.Async.Tasks.Handling_Exceptions
{
    public class HandlingExceptionsUsingTryCatch : IDisposable
    {
        private Task<List<int>> _taskWithFactoryAndState;

        public HandlingExceptionsUsingTryCatch()
        {
            // create the task
            _taskWithFactoryAndState = Task.Factory.StartNew((stateObj) =>
            {
                var ints = new List<int>();
                for (int i = 0; i < (int)stateObj; i++)
                {
                    ints.Add(i);
                    if (i > 100)
                    {
                        var ex = new InvalidOperationException("oh no its > 100");
                        ex.Source = "taskWithFactoryAndState";
                        throw ex;
                    }
                }
                return ints;
            }, 2000);

            _taskWithFactoryAndState.ContinueWith((task, obj) =>
            {
                if (!_taskWithFactoryAndState.IsFaulted)
                {
                    Console.WriteLine("managed to get {0} items", _taskWithFactoryAndState.Result.Count);
                }

                /*try
                {
                    //use one of the trigger methods (ie Wait() to make sure AggregateException
                    //is observed)
                    _taskWithFactoryAndState.Wait();
                    if (!_taskWithFactoryAndState.IsFaulted)
                    {
                        Console.WriteLine("managed to get {0} items", _taskWithFactoryAndState.Result.Count);
                    }
                }
                catch (AggregateException aggEx)
                {
                    foreach (Exception ex in aggEx.InnerExceptions)
                    {
                        Console.WriteLine("Caught exception '{0}'", ex.Message);
                    }
                }*/
            }, null);
        }

        void IDisposable.Dispose()
        {
            Console.WriteLine("All done, press Enter to Quit");
            Console.ReadLine();

            _taskWithFactoryAndState.Dispose();
        }

    }
}
