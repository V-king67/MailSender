using MailSender.lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.lib.Interfaces
{
    public interface IMailSchedulerService
    {
        void Start();
        void Stop();
        void AddTask(DateTime time, Sender sender, IEnumerable<Recipient> recipients, Server server, Message message);
    }
}
