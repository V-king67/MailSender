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
            mainThread.Name = "main";

            var timerThread = new Thread(Timer);
            timerThread.IsBackground = true; //Поток завершится по завершении главного потока
            timerThread.Start();

            for (int i = 0; i < 500; i++)
            {
                Console.WriteLine("Главный поток {0}", i);
                Thread.Sleep(10);
            }

            Console.WriteLine("Главный поток работу закончил!");
            Console.ReadLine();
        }

        static void Timer()
        {
            PrintThreadInfo();
            while (true)
            {
                Console.Title = DateTime.Now.ToString("HH:mm:ss.ffff");
                Thread.Sleep(100);
                //Thread.SpinWait(10); //Используется на малое время чтобы поток подождал, не приостанавливает поток
            }
        }

        static void PrintThreadInfo()
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine("id: {0}; name: {1}; priority: {2}", thread.ManagedThreadId, thread.Name, thread.Priority);
        }
    }
}
