using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.lib.Service
{
    public class TaskMailSchedulerService : IMailSchedulerService
    {
        private readonly IStore<SchedulerTask> _tasksStore;
        private readonly IMailService _mailService;
        private Task _schedulerTask;
        private readonly CancellationTokenSource _taskCancellation = new CancellationTokenSource();

        public TaskMailSchedulerService(IStore<SchedulerTask> tasksStore, IMailService mailService)
        {
            _tasksStore = tasksStore;
            _mailService = mailService;
        }

        public void Start()
        {
            //var schedulerTask = Task.Run(SchedulerTaskMethod);
            _schedulerTask = Task.Factory.StartNew(SchedulerTaskMethodAsync, TaskCreationOptions.LongRunning);
        }

        public void Stop() => _taskCancellation.Cancel();


        private async Task SchedulerTaskMethodAsync()
        {
            var cancel = _taskCancellation.Token;
            while (true)
            {
                cancel.ThrowIfCancellationRequested();
                var nextTask = _tasksStore.GetAll()
                    .OrderBy(t => t.Time)
                    .FirstOrDefault(t => t.Time > DateTime.Now);

                if (nextTask is null) break;

                var sleepTime = nextTask.Time - DateTime.Now;

                if(sleepTime.TotalMilliseconds > 0)
                    await Task.Delay(sleepTime, cancel);

                cancel.ThrowIfCancellationRequested();
                await Execute(nextTask);
            }
        }

        private async Task Execute(SchedulerTask task)
        {
            var server = task.Server;
            var sender = _mailService.GetSender(server.Address, server.Port, server.UseSSL, server.Login, server.Password);
            await sender.SendAsync(task.Sender.Address, task.Recipients.Select(r => r.Address), task.Message.Subject, task.Message.Body);
        }

        public void AddTask(DateTime time, Sender sender, IEnumerable<Recipient> recipients, Server server, Message message)
        {
            //проверить в дебаге
            Stop();

            _tasksStore.Add(new SchedulerTask
            {
                Message = message,
                Sender = sender,
                Server = server,
                Recipients = recipients.ToArray(),
                Time = time
            });

            Start();
        }
    }
}
