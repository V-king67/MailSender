using System;
using System.Reflection.Metadata;
using System.Threading;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadsTests.TestStart();

            Console.WriteLine("Главный поток работу закончил!");
            Console.ReadLine();
        }
    }
}
