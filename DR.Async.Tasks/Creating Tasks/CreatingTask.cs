using System.Collections.Generic;
using System.Threading.Tasks;

namespace DR.Async.Tasks
{
    public class CreatingTask
    {
        private Task<List<int>> _taskWithInLineAction;
        private Task<string> _taskWithInActualMethodAndState;
        private Task<List<int>> _taskWithFactoryAndState;

        public CreatingTask()
        {
            // *****************************************************************
            // OPTION 1 : Create a Task using an inline action
            // *****************************************************************
            _taskWithInLineAction = new Task<List<int>>(() =>
            {
                var ints = new List<int>();
                for (int i = 0; i < 1000; i++)
                {
                    ints.Add(i);
                }
                return ints;

            });

            // *****************************************************************
            // OPTION 2 : Create a Task that calls an actual method that returns a string
            // *****************************************************************
            _taskWithInActualMethodAndState = new Task<string>(() => string.Empty);

            // *****************************************************************
            // OPTION 3 : Create and start a Task that returns List<int> using Task.Factory
            // *****************************************************************
            _taskWithFactoryAndState = Task.Factory.StartNew(stateObj =>
            {
                var ints = new List<int>();
                for (int i = 0; i < (int)stateObj; i++)
                {
                    ints.Add(i);
                }
                return ints;
            }, 2000);
        }

        public void StartTask()
        {
            _taskWithInLineAction.Start();
            _taskWithInActualMethodAndState.Start();
        }
    }
}
