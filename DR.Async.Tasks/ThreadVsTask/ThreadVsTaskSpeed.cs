using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DR.Async.Tasks.ThreadVsTask
{
    public class ThreadVsTaskSpeed : IDisposable
    {
        private int maxWaitHandleWaitAllAllowed = 64;
        private Stopwatch watch;
        private long threadTime = 0;
        private long taskTime = 0;
        private ManualResetEventSlim[] mres;

        public int MaxThreads
        {
            get { return maxWaitHandleWaitAllAllowed; }
            set { maxWaitHandleWaitAllAllowed = value; }
        }

        public ThreadVsTaskSpeed()
        {
            watch = new Stopwatch();
            //64 is upper limit for WaitHandle.WaitAll() method
            mres = new ManualResetEventSlim[maxWaitHandleWaitAllAllowed];
            for (int i = 0; i < mres.Length; i++)
            {
                mres[i] = new ManualResetEventSlim(false);
            }
        }

        public void StartThreads()
        {
            watch.Start();
            for (int i = 0; i < mres.Length; i++)
            {
                int idx = i;
                var thread = new Thread((state) =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Console.WriteLine(string.Format("Thread : {0}, outputing {1}",
                            state.ToString(), j.ToString()));
                    }
                    mres[idx].Set();
                });
                thread.Start(string.Format("Thread{0}", i));
            }


            WaitHandle.WaitAll((from x in mres select x.WaitHandle).ToArray());
            threadTime = watch.ElapsedMilliseconds;
            watch.Reset();

            for (int i = 0; i < mres.Length; i++)
            {
                mres[i].Reset();
            }
        }

        public void StartTasks()
        {
            watch.Start();

            for (int i = 0; i < mres.Length; i++)
            {
                int idx = i;
                var task = Task.Factory.StartNew((state) =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Console.WriteLine(string.Format("Task : {0}, outputing {1}",
                            state.ToString(), j.ToString()));
                    }
                    mres[idx].Set();
                }, string.Format("Task{0}", i));
            }

            WaitHandle.WaitAll((from x in mres select x.WaitHandle).ToArray());
            taskTime = watch.ElapsedMilliseconds;

            Console.WriteLine("Thread Time waited : {0}ms", threadTime);
            Console.WriteLine("Task Time waited : {0}ms", taskTime);

            for (int i = 0; i < mres.Length; i++)
            {
                mres[i].Reset();
            }
        }

        void IDisposable.Dispose()
        {
            Console.WriteLine("All done, press Enter to Quit");
            Console.ReadLine();
        }

    }
}
