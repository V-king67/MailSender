using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    static class ParallelTests
    {
        public static void Start()
        {
            Parallel.Invoke(
            new ParallelOptions { MaxDegreeOfParallelism = 2 },
            ParallelInvokeMethod,
            ParallelInvokeMethod,
            ParallelInvokeMethod,
            ParallelInvokeMethod,
            ParallelInvokeMethod,
            ParallelInvokeMethod,
            ParallelInvokeMethod,
            () => Console.WriteLine("One more method"));

            Parallel.For(0, 40, new ParallelOptions { MaxDegreeOfParallelism = 2 }, i =>
            {
                Console.WriteLine(i);
                Thread.Sleep(100);
            });

            var messages = Enumerable.Range(1, 150).Select(i => $"Message {i}");
            Parallel.ForEach(messages, message =>
            {
                Console.WriteLine(message);
                Thread.Sleep(100);
            });

            var messagesCount = messages
                .AsParallel()
                .WithDegreeOfParallelism(2)
                .Where(msg => msg.EndsWith("0"))
                .AsSequential()
                .Count();

            Console.WriteLine("Сообщений с круглым номером: {0}", messagesCount);

        }
        private static void ParallelInvokeMethod()
        {
            Console.WriteLine("ThrID: {0} - started", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(500);
            Console.WriteLine("ThrID: {0} - finished", Thread.CurrentThread.ManagedThreadId);
        }

        private static void ParallelInvokeMethod(string msg)
        {
            Console.WriteLine("ThrID: {0} - started: {1}", Thread.CurrentThread.ManagedThreadId, msg);
            Thread.Sleep(500);
            Console.WriteLine("ThrID: {0} - finished: {1}", Thread.CurrentThread.ManagedThreadId, msg);
        }

    }
}
