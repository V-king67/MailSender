using System;
using System.Threading;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainThread = Thread.CurrentThread;
            var mainThreadId = mainThread.ManagedThreadId;
            mainThread.Name = "Основной поток";

            var timerThread = new Thread(TimerMethod);
            timerThread.Name = "Поток часов";
            timerThread.IsBackground = true;
            timerThread.Start();

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(10);
            }
        }

        static void TimerMethod()
        {
            PrintThreadInfo();
            while (true)
            {
                Console.Title = DateTime.Now.ToString("HH:mm:ss.ff");
                Thread.Sleep(500);
            }
        }

        static void PrintThreadInfo()
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine("id: {0} name: {1} priority: {2}", thread.ManagedThreadId, thread.Name, thread.Priority);
        }
    }
}
