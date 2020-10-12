using System;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadTests.Start();
            //CriticalSectionTests.Start();
            ThreadPoolTests.Start();
            Console.WriteLine("Основной поток работу завершил!");
            Console.ReadLine();
        }
    }
}
