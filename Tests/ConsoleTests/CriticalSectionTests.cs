using System;
using System.IO;
using System.Threading;

namespace ConsoleTests
{
    static class CriticalSectionTests
    {
        public static void Start()
        {
            LockSyncTest();

            var manualResetEvent = new ManualResetEvent(false);
            var autoResetEvent = new AutoResetEvent(false);

            EventWaitHandle starter = autoResetEvent;

            for (int i = 0; i < 10; i++)
            {
                var local_i = i;
                new Thread(() =>
                {
                    Console.WriteLine("Поток {0} запущен", local_i);
                    starter.WaitOne(); //тормозит текущий поток
                    //starter.Reset(); //нужен для ManualResetEvent
                    Console.WriteLine("Поток {0} завершил работу", local_i);
                }).Start();
            }

            Console.WriteLine("Все потоки созданы.");
            Console.ReadLine();

            starter.Set(); //запускает первый из остановленных с помощью WaitOne потоков

            Console.ReadLine();

            //Мьютексы и семафоры

            //var mutex1 = new Mutex(true, "Test mutex", out var created1);
            //var mutex2 = new Mutex(true, "Test mutex", out var created2);
            //mutex1.WaitOne();
            //mutex1.ReleaseMutex();

            //var semaphore = new Semaphore(0, 10); //mutex == semaphore(0,1)
            //semaphore.WaitOne();
            //semaphore.Release();
        }

        private static void LockSyncTest()
        {
            var threads = new Thread[10];
            for (int i = 0; i < threads.Length; i++)
            {
                var local_i = i;
                threads[i] = new Thread(() => PrintData($"Message from thread {local_i}", 10));
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

    public class FileLogger : ContextBoundObject
    {
        readonly string _logFileName;

        public FileLogger(string logFileName) => _logFileName = logFileName;

        public void Log(string message)
        {
            File.WriteAllText(_logFileName, message);
        }
    }
}
