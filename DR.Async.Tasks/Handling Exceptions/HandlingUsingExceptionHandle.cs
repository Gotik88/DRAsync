using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DR.Async.Tasks.Handling_Exceptions
{
    public class HandlingUsingExceptionHandle : IDisposable
    {
        private readonly Task<List<int>> _taskWithFactoryAndState;

        public HandlingUsingExceptionHandle()
        {
            _taskWithFactoryAndState = Task.Factory.StartNew(stateObj =>
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

            try
            {
                _taskWithFactoryAndState.Wait(); // re throw exception 
                if (!_taskWithFactoryAndState.IsFaulted)
                {
                    Console.WriteLine("managed to get {0} items", _taskWithFactoryAndState.Result.Count);
                }
            }
            catch (AggregateException aggEx)
            {
                aggEx.Handle(HandleException);
            }
        }

        private static bool HandleException(Exception ex)
        {
            if (ex is InvalidOperationException)
            {
                Console.WriteLine(string.Format("Caught exception '{0}'", ex.Message));
                return true;
            }
            else
            {
                return false;
            }
        }

        void IDisposable.Dispose()
        {
            Console.WriteLine("All done, press Enter to Quit");
            Console.ReadLine();

            _taskWithFactoryAndState.Dispose();
        }
    }
}
