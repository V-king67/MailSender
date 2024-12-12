using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleTests
{
    static class ThreadPoolTests
    {
        public static void Start()
        {
            var messages = Enumerable.Range(1, 600)
                .Select(i => $"Message {i}")
                .ToArray();

            var timer = Stopwatch.StartNew();
            for (int i = 0; i < messages.Length; i++)
            {
                //var local_i = i;
                //new Thread(() => ProcessMessage(messages[local_i])) { IsBackground = true }.Start();
                ThreadPool.QueueUserWorkItem(o => ProcessMessage((string)o), messages[i]);
            }


            timer.Stop();
            Console.Title = "Обработка заняла " + timer.Elapsed.TotalSeconds;
        }

        static void ProcessMessage(string message)
        {
            Console.WriteLine("Обработка сообщения {0}", message);
            Thread.Sleep(3000);
            Console.WriteLine("Обработка сообщения {0} закончена", message);
        }
    }
}
