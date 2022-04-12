using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace MailSender.lib.Service
{
    public class DataStorageInMemory : IStorage<Message>, IStorage<Recipient>, IStorage<Sender>, IStorage<Server>
    {
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();
        public ICollection<Sender> Senders { get; set; } = new List<Sender>();
        public ICollection<Server> Servers { get; set; } = new List<Server>();

        ICollection<Message> IStorage<Message>.Items => Messages;
        ICollection<Recipient> IStorage<Recipient>.Items => Recipients;
        ICollection<Sender> IStorage<Sender>.Items => Senders;
        ICollection<Server> IStorage<Server>.Items => Servers;


        public void Load(ICollection<Message> messages)
        {
            Debug.WriteLine("Вызвана процедура загрузки сообщений");

            if (Messages is null || Messages.Count == 0)
                Messages = messages;
        }
        public void Load(ICollection<Recipient> recipients)
        {
            Debug.WriteLine("Вызвана процедура загрузки получателей");

            if (Recipients is null || Recipients.Count == 0)
                Recipients = recipients;
        }
        public void Load(ICollection<Sender> senders)
        {
            Debug.WriteLine("Вызвана процедура загрузки отправителей");

            if (Senders is null || Senders.Count == 0)
                Senders = senders;
        }
        public void Load(ICollection<Server> servers)
        {
            Debug.WriteLine("Вызвана процедура загрузки серверов");

            if (Servers is null || Servers.Count == 0)
                Servers = servers;
        }

        public void SaveChanges()
        {
            Debug.WriteLine("Вызвана процедура сохранения данных");
        }
    }
}
