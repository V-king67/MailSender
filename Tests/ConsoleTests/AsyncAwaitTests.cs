using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    internal static class AsyncAwaitTests
    {
        public static async Task StartAsync()
        {
            Console.WriteLine("Запуск асинхронного метода выполняется синхронно!");
            PrintThreadInfo("При входе в метод StartAsync");

            //var resultTask = GetStringResultAsync();
            //var result = await resultTask;
            
            for (int i = 0; i < 20; i++)
            {
                var result = await GetStringResultReallyAsync();

                Console.WriteLine("Результат получен: {0}", result);
                PrintThreadInfo("При печати результата");
            } 
        }


        static async Task<string> GetStringResultAsync()
        {
            PrintThreadInfo("В псевдоасинхронном методе");
            return DateTime.Now.ToString(); //async сам оборачивает возвращаемое значение в Task
        }

        //Эквивалентный метод
        static Task<string> GetStringResult()
        {
            return Task.FromResult(DateTime.Now.ToString());
        }

        static Task<string> GetStringResultReallyAsync()
        {
            PrintThreadInfo("В начале асинхронного метода");
            return Task.Run(() =>
            {
                PrintThreadInfo("Внутри асинхронного метода");
                Thread.Sleep(500);
                return DateTime.Now.ToString();
            });
        }

        static void PrintThreadInfo(string message = "")
        {
            var currentThread = Thread.CurrentThread;
            Console.WriteLine("Thread ID: {0}   Task ID: {1}    {2}", currentThread.ManagedThreadId, Task.CurrentId, message);
        }

        public static async Task ProcessDataTestAsync()
        {
            //Перечисление строк
            var messages = Enumerable.Range(1, 50).Select(i => $"Message {i}");//.ToArray();
            //Перечисление задач. Задачи не созданы!
            var tasks = messages.Select(msg => Task.Run(() => LowSpeedPrinter(msg)));
            //Для создания задач их нужно вызвать, например, поместив в массив
            Console.WriteLine("Подготовка к запуску обработки сообщений...");
            var runningTasks = tasks.ToArray();
            Console.WriteLine("Задачи созданы");

            await Task.WhenAll(runningTasks);

            Console.WriteLine("Обработка всех сообщений завершена!");
        }

        static void LowSpeedPrinter(string msg)
        {
            Console.WriteLine("[Thread ID: {1}] Начинаю обработку {0}...", msg, Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(100);
            Console.WriteLine("[Thread ID: {1}] Сообщение {0} обработано!", msg, Thread.CurrentThread.ManagedThreadId);
        }
    }
}
