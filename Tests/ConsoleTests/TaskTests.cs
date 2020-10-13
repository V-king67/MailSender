using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    class TaskTests
    {
        public static void Start(int switcher)
        {
            Task printTask;
            switch (switcher)
            {
                default:
                    break;
                    //----------------------------------------//
                case 0:
                    printTask = new Task(() => Console.WriteLine("Hello world!"));
                    printTask.Start();

                    var continuationTask = printTask.ContinueWith(
                        t => Console.WriteLine("Задача {0} завершилась.", t.Id),
                        TaskContinuationOptions.OnlyOnRanToCompletion);
                    break;
                    //----------------------------------------//
                case 1:
                    printTask = Task.Run(() => Console.WriteLine("Hello world!")); //Оптимальный вариант использования Task
                    break;
                //----------------------------------------//
                case 2:
                    printTask = Task.Factory.StartNew(() => Console.WriteLine("Hello world!")); //Использование Task с параметрами
                    break;
            }

            var resultTask = Task.Run(() =>
            {
                Thread.Sleep(100);
                return 4;   //Task с возвращаемым значением
            });

            var resultTask2 = Task.Run(() =>
            {
                Thread.Sleep(100);
                return 4;   //Task с возвращаемым значением
            });

            var result = resultTask.Result; //Не рекомендуется использовать из-за вероятности мертвой блокировки

            Task.WaitAll(resultTask, resultTask2); //Не рекомендуется использовать из-за вероятности мертвой блокировки
            var completedIndex = Task.WaitAny(resultTask, resultTask2); //Вернет индекс первой завершившейся задачи

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
            })
            { IsBackground = true };
        }

        public void Start() => _calculationThread.Start();
    }
}
