using System;
using System.Threading;

namespace ConsoleTests
{
    static class ThreadTests
    {
        public static void Start()
        {
            var mainThread = Thread.CurrentThread;
            var mainThreadId = mainThread.ManagedThreadId;
            mainThread.Name = "Основной поток";

            var timerThread = new Thread(TimerMethod)
            {
                Name = "Поток часов",
                IsBackground = true
            };
            timerThread.Start();

            var message = "Hello world!";
            var count = 10;

            var printTask = new PrintMessageTask(message, count);
            printTask.Start(); //Передача информации в поток с использованием ООП

            //new Thread(() => PrintMessage(message, count)) { IsBackground = true }.Start();

            //var printerThread = new Thread(PrintMessage)
            //{
            //    IsBackground = true,
            //    Name = "Parameter printer"
            //};
            //printerThread.Start("Hello world!");

            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine(i);
            //    Thread.Sleep(10);
            //}

            Console.WriteLine("Останавливаю время...");

            //Алгоритм остановки потока
            __timerWorking = false;
            if (!timerThread.Join(100))
                timerThread.Interrupt();
        }

        static void PrintMessage(string message, int count)
        {
            PrintThreadInfo();
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("id: {0}\t{1}", Thread.CurrentThread.ManagedThreadId, message);
                Thread.Sleep(10);
            }
        }

        static volatile bool __timerWorking = true;
        static void TimerMethod()
        {
            PrintThreadInfo();
            while (__timerWorking)
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
    class PrintMessageTask
    {
        string _message;
        int _count;
        Thread _thread;

        public PrintMessageTask(string message, int count)
        {
            _message = message;
            _count = count;
            _thread = new Thread(ThreadMethod) { IsBackground = true };
        }

        public void Start()
        {
            if (_thread?.IsAlive == false)
                _thread?.Start();
        }

        void ThreadMethod()
        {
            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine("id: {0}\t{1}", Thread.CurrentThread.ManagedThreadId, _message);
                Thread.Sleep(10);
            }
            _thread = null;
        }
    }
}
