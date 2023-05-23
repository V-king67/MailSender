using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleTests
{
    internal static class CriticalSectionTests
    {
        public static void Start()
        {
            var threads = new Thread[10];
            for (int i = 0; i < threads.Length; i++)
            {
                var local_i = i; //Без локальной переменной в цикле создается замыкание Лямбда-выражения (переменная i будет идентична для всех созданных делегатов)
                threads[i] = new Thread(() => PrintData($"Message from thread {local_i}", 10));
            }
            for (int i = 0; i < threads.Length; i++)
                threads[i].Start();
        }

        static void PrintData(string message, int count)
        {
            for (int i = 0; i < count; i++)
            {
                lock (__SyncRoot) //Обеспечивает непрерывное выполнение фрагмента кода внутри потока
                {
                    Console.Write("Thread id: {0}", Thread.CurrentThread.ManagedThreadId);
                    Console.Write("\t");
                    Console.Write(message);
                    Console.WriteLine(); 
                }
            }
        }

        //Обертка для конструкции Lock - статическое поле ссылочного типа

        static readonly object __SyncRoot = new object();


        /// <summary>
        /// Что происходит внутри конструкции lock
        /// </summary>
        static void LockUnfolded()
        {
            Monitor.Enter(__SyncRoot);
            try
            {
                //Здесь код
            }
            finally
            {
                Monitor.Exit(__SyncRoot);
            }
        }
    }
}
