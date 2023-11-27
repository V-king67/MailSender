using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    internal static class ParallelTests
    {
        public static void Start()
        {
            Action<string> printer = str =>
            {
                Console.WriteLine("Thread ID:{1,2}  Message: {0}", str, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(100);
            };

            Parallel.Invoke(
                new ParallelOptions { MaxDegreeOfParallelism = 3 },
                ParallelInvokeMethod,
                ParallelInvokeMethod,
                ParallelInvokeMethod,
                ParallelInvokeMethod,
                ParallelInvokeMethod,
                () => Console.WriteLine("Lambda Method :)"));

            //Parallel.For(0, 100, i => printer(i.ToString()));
            //Parallel.For(0, 100, new ParallelOptions { MaxDegreeOfParallelism = 3 }, i => printer(i.ToString()));

            // -- Прерывание параллельного цикла --
            var loopResult = Parallel.For(0, 100, new ParallelOptions { MaxDegreeOfParallelism = 3 }, (i, state) => 
            {
                printer(i.ToString());
                if (i > 10)
                    state.Break();
            });
            //  Вытаскиваем индекс последней итерации
            Console.WriteLine("Выполнено {0} итераций", loopResult.LowestBreakIteration);

            //  Работа с массивами данных 
            var messages = Enumerable.Range(1, 500).Select(i => $"Message {i}");//.ToArray();

            //Parallel.ForEach(messages, message => printer(message));

            var messagesCount = messages
                .AsParallel()
                .WithDegreeOfParallelism(4)
                .Where(msg =>
                {
                    printer (msg);
                    return msg.EndsWith("0");
                }).Count();
                


        }

        static void ParallelInvokeMethod()
        {
            Console.WriteLine("ThreadID: {0} - started", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(300);
            Console.WriteLine("ThreadID: {0} - finished", Thread.CurrentThread.ManagedThreadId);
        }
        static void ParallelInvokeMethod(string msg)
        {
            Console.WriteLine("ThreadID: {0} - started : {1}", Thread.CurrentThread.ManagedThreadId, msg);
            Thread.Sleep(300);
            Console.WriteLine("ThreadID: {0} - finished : {1}", Thread.CurrentThread.ManagedThreadId, msg);
        }

    }
}
