using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

        public void Load()
        {
            Debug.WriteLine("Вызвана процедура загрузки данных");

            if (Senders is null || Senders.Count == 0)
                Senders = new List<Sender>(
                    Enumerable.Range(1, 10)
                    .Select(i => new Sender
                    {
                        Name = $"Отправитель {i}",
                        Address = $"Sender_{i}@server.ru"
                    }).ToList());

            if (Recipients is null || Recipients.Count == 0)
                Recipients = new List<Recipient>(
                    Enumerable.Range(1, 10)
                    .Select(i => new Recipient
                    {
                        Name = $"Получатель {i}",
                        Address = $"Recipient_{i}@server.ru"
                    }).ToList());

            if (Servers is null || Servers.Count == 0)
                Servers = new List<Server>(
                    Enumerable.Range(1, 10)
                    .Select(i => new Server
                    {
                        Name = $"Сервер #{i}",
                        Address = $"smtp.server{i}.ru",
                        Login = $"Login-{i}",
                        Password = TextEncoder.Encode($"Password-{i}"),
                        UseSSL = i % 2 == 0,
                        Description = $"Описание сервера {i}"
                    }).ToList());

            if (Messages is null || Messages.Count == 0)
                Messages = new List<Message>(
                    Enumerable.Range(5, 30)
                    .Select(i => new Message
                    {
                        Subject = $"Сообщение {i}",
                        Body = $"Текст сообщения {i}"
                    }).ToList());
        }

        public void SaveChanges()
        {
            Debug.WriteLine("Вызвана процедура сохранения данных");
        }
    }
}
