using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    static class AsyncAwaitTests
    {
        public static async Task StartAsync()
        {
            PrintThreadInfo("При входе в метод StartAsync");
            //var resultTask = GetStringAsync();
            //var result = await resultTask;
            var result = await GetStringReallyAsync();

            Console.WriteLine("Был получен результат {0}", result);
            PrintThreadInfo("При печати результата");
        }

        static void PrintThreadInfo(string message = "")
        {
            var currentThread = Thread.CurrentThread;
            Console.WriteLine("Thread id: {0}\t Task id: {1}\t{2}", currentThread.ManagedThreadId, Task.CurrentId, message);
        }

        static async Task<string> GetStringAsync()
        {
            PrintThreadInfo("В псевдоасинхронном методе");
            return "Task result";
            //return Task.FromResult("Task result");
        }
        static Task<string> GetStringReallyAsync()
        {
            PrintThreadInfo("В начале асинхронного метода");
            return Task.Run(() =>
            {
                PrintThreadInfo("Внутри асинхронного метода");
                return "Task result";
            });
        }

        public static async Task ProcessDataTestAsync()
        {
            var messages = Enumerable.Range(1, 150).Select(i => $"Message {i}");
            var tasks = messages.Select(msg => Task.Run(() => LowSpeedPrinter(msg)));

            Console.WriteLine("Подготовка к запуску обработки сообщений...");
            var runningTasks = tasks.ToArray();

            await Task.WhenAll(runningTasks);
            Console.WriteLine("Обработка всех сообщений завершена");

        }

        static void LowSpeedPrinter(string msg)
        {
            Console.WriteLine("[Thread id:{1}] {0} starting...", msg, Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(100);
            Console.WriteLine("[Thread id:{1}] {0} processed", msg, Thread.CurrentThread.ManagedThreadId);
        }
    }
}
