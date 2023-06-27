using System;
using System.Reflection.Metadata;
using System.Threading;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadsTests.TestStart();
            //CriticalSectionTests.Start();
            //MutexTests.Start();
            ThreadPoolTests.Start();

            Console.WriteLine("Главный поток работу закончил!");
            Console.ReadLine();
        }
    }
}
