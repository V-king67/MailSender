using System;
using System.Threading;

namespace ConsoleTests
{
    static class CriticalSectionTests
    {
        public static void Start()
        {
            LockSyncTest();
        }

        private static void LockSyncTest()
        {
            var threads = new Thread[10];
            for (int i = 0; i < threads.Length; i++)
            {
                var localI = i;
                threads[i] = new Thread(() => PrintData($"Message from thread {localI}", 10));
            }
            for (int i = 0; i < threads.Length; i++)
                threads[i].Start();
        }

        static readonly object __syncRoot = new object();

        static void PrintData(string message, int count)
        {
            for (int i = 0; i < count; i++)
            {
                lock (__syncRoot)
                {
                    Console.Write("Thread id: {0}", Thread.CurrentThread.ManagedThreadId);
                    Console.Write("\t");
                    Console.WriteLine(message);
                }

                #region Развернутый lock
                //Monitor.Enter(__syncRoot);
                //try
                //{
                //    Console.Write("Thread id: {0}", Thread.CurrentThread.ManagedThreadId);
                //    Console.Write("\t");
                //    Console.WriteLine(message);
                //}
                //finally
                //{
                //    Monitor.Exit(__syncRoot);
                //}
                #endregion
            }
        }
    }


}
