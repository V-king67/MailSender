using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    internal static class TasksTests
    {
        public static void Start()
        {
            Action<string> printer = str =>
            {
                Console.WriteLine("Thread ID:{1,2}  Message: {0}", str, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(100);
            };

            // -- Старомодное создание задач --

            var task = new Task(() => printer("Hello world!"));

            var status = task.Status;

            task.Start();

            var nextTask = task.ContinueWith(
                t => Console.WriteLine("Задача {0} завершилась", t.Id),
                TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith(
                t => Console.WriteLine("Задача {0} прервана", t.Id),
                TaskContinuationOptions.NotOnRanToCompletion);
            nextTask.ContinueWith(t => Console.WriteLine("OK"));


            // -- БАЗА --

            Task.Run(() => printer("Based hello world!"));
            Task.Factory.StartNew(obj => printer((string)obj), "Factory Hello world!"); //Более медленный метод с возможностью добавлять опции, в т.ч. параметры в делегат

            //Задача с результатом 

            var resultTask = Task.Run(() =>
            {
                Thread.Sleep(100);
                return 42;
            });

            // -- /база -- 

            var result = resultTask.Result; //Костыли! Не рекомендуется к использованию из-за вероятности мертвой блокировки

            var resultTask2 = Task.Run(() =>
            {
                Thread.Sleep(500);
                return 50;
            }); 
            var resultTask3 = Task.Run(() =>
            {
                Thread.Sleep(10);
                return 13;
            });

            Task.WaitAll(resultTask, resultTask2, resultTask3); //Костыли! Не рекомендуется к использованию из-за вероятности мертвой блокировки
        }
    }
}
