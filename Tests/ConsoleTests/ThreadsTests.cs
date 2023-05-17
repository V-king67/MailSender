using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleTests
{
    internal static class ThreadsTests
    {
        public static void TestStart()
        {
            var mainThread = Thread.CurrentThread;
            var mainThreadId = mainThread.ManagedThreadId;
            mainThread.Name = "main";

            var timerThread = new Thread(Timer)
            {
                IsBackground = true,        //Поток завершится по завершении главного потока
                Name = "Timer"
            };
            timerThread.Start();

            var printerThread = new Thread(PrintMessage)
            {
                IsBackground = true,
                Name = "Parameter printer"
            };
            printerThread.Start("Hello world!");        //Для методов с одним параметром, параметр передается в метод Start(parameter)

            var message = "Hello world~!";
            var count = 10;
            new Thread(() => PrintMessage(message, count)) { IsBackground = true, Name = "MultiParameter printer" }.Start();

            //Создание потока в классе
            var printTask = new PrintMessageTask("Message from class", count);
            printTask.Start();

            //Поток в производном классе
            var greeting = new GreetingTask("Greeter");
            greeting.Start();

            /*for (int i = 0; i < 500; i++)
            {
                Console.WriteLine("Главный поток {0}", i);
                Thread.Sleep(10);
            }*/

            Console.WriteLine("Останавливаю время...");

            //Алгоритм остановки потока
            timerWork = false;
            if (!timerThread.Join(100))       //Основной поток приостанавливается, пока вызывающий не закончит работу (или не выйдет кол-во времени в параметре)
                timerThread.Interrupt();

            Console.ReadLine();
        }


        static volatile bool timerWork = true;       //флаг для остановки потока
                                                     //volatile сообщает компилятору, что переменная будет использоваться в нескольких потоках
                                                     //чтобы компилятор не изменял / не оптимизировал ее на стадии сборки/выполнения
        static void Timer()
        {
            Console.WriteLine("Поток, обрабатывающий таймер: ");
            PrintThreadInfo();
            while (timerWork)
            {
                Console.Title = DateTime.Now.ToString("HH:mm:ss.ffff");
                Thread.Sleep(100);
                //Thread.SpinWait(10);      //Используется на малое время чтобы поток подождал, не приостанавливает поток
            }
        }

        static void PrintThreadInfo()
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine("id: {0}; name: {1}; priority: {2}", thread.ManagedThreadId, thread.Name, thread.Priority);
        }

        static void PrintMessage(object parameter)
        {
            PrintThreadInfo();
            var msg = (string)parameter;
            var threadId = Thread.CurrentThread.ManagedThreadId;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("id: {0}\t{1}", threadId, msg);
                Thread.Sleep(10);
            }
        }

        static void PrintMessage(string message, int count)
        {
            PrintThreadInfo();
            var threadId = Thread.CurrentThread.ManagedThreadId;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("id: {0}\t{1}", threadId, message);
                Thread.Sleep(10);
            }
        }
    }

    //Архитектурно правильный способ
    class PrintMessageTask
    {
        private readonly string message;
        private readonly int count;
        private Thread thread;

        public PrintMessageTask(string message, int count)
        {
            this.message = message;
            this.count = count;
            thread = new Thread(ThreadMethod) { IsBackground = true, Name = "Incapsulated printer" };
        }

        public void Start()
        {
            if (thread?.IsAlive == false)
                thread?.Start();
        }

        void ThreadMethod()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("id: {0}\t{1}", threadId, message);
                Thread.Sleep(10);
            }

            thread = null;      //Повторный запуск невозможен
        }
    }

    //Пробую обобщить в абстрактный класс
    abstract class EncasedThread
    {
        public Thread Thread { get; set; }

        public EncasedThread()
        {
            Thread = new Thread(ThreadMethod);
        }

        public void Start()
        {
            if (Thread?.IsAlive == false)
                Thread?.Start();
        }

        protected abstract void ThreadMethod();
    }

    class GreetingTask : EncasedThread
    {
        private string yourName;

        public GreetingTask(string threadName) : base()
        {
            Thread.Name = threadName;
        }

        protected override void ThreadMethod()
        {
            Console.WriteLine("Thread {0} started", Thread.Name);
            Console.Write("Insert your name: ");
            yourName = Console.ReadLine();
            Console.WriteLine("Hello, {0}", yourName);
            Console.WriteLine("Thread {0} finished", Thread.Name);
        }
    }
}
