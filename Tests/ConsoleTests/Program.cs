using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {


            //ThreadTests.Start();
            //CriticalSectionTests.Start();
            //ThreadPoolTests.Start();
            //ParallelTests.Start();
            TaskTests.Start(0);

            Console.WriteLine("Основной поток работу завершил!");
            Console.ReadLine();
        }

    }
}
