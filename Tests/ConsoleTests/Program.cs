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
            ParallelTests.Start();

            Console.WriteLine("Основной поток работу завершил!");
            Console.ReadLine();
        }

    }

    class MathTask //Имитируем класс Task
    {
        private readonly Thread _calculationThread;
        private int _result;
        private bool _isCompleted;

        public bool IsCompleted => _isCompleted;
        public int Result
        {
            get
            {
                if (!_isCompleted)
                    _calculationThread.Join();
                return _result;
            }
        }

        public MathTask(Func<int> calculation)
        {
            _calculationThread = new Thread(() =>
            {
                _result = calculation();
                _isCompleted = true;
            }) { IsBackground = true };
        }

        public void Start() => _calculationThread.Start();
    }
}
