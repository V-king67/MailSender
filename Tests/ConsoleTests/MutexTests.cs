using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleTests
{
    internal static class MutexTests
    {
        public static void Start()
        {
            MutexTest();
        }

        static void MutexTest()
        {
            var mutex1 = new Mutex(true, "Test mutex", out var created1);
            var mutex2 = new Mutex(true, "Test mutex", out var created2);

            mutex1.WaitOne(); 
            mutex1.ReleaseMutex();

            var semaphore = new Semaphore(1, 10);
            semaphore.WaitOne();
            semaphore.Release();
        }

    }
}
