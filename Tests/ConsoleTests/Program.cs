using System;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadsTests.TestStart();
            //CriticalSectionTests.Start();
            //MutexTests.Start();
            //ThreadPoolTests.Start();
            //ParallelTests.Start();
            //TasksTests.Start();
            var testTask = AsyncAwaitTests.StartAsync();
            var processDataTask = AsyncAwaitTests.ProcessDataTestAsync();

            Console.WriteLine("Тестовая задача запущена и мы ее ждем...");

            Task.WaitAll(testTask, processDataTask);

            Console.WriteLine("Главный поток работу закончил!");
            Console.ReadLine();
        }
    }
}
