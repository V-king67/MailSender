using MailSender.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Data
{
    static class TestData
    {
        public static List<Sender> Senders { get; } = Enumerable.Range(1, 10)
            .Select(i => new Sender
            {
                Name = $"Отправитель {i}",
                Address = $"Sender_{i}@server.ru"
            }).ToList();

        public static List<Recipient> Recipients { get; } = Enumerable.Range(1, 10)
            .Select(i => new Recipient
            {
                Name = $"Получатель {i}",
                Address = $"Recipient_{i}@server.ru"
            }).ToList();

        public static List<Server> Servers { get; } = Enumerable.Range(1, 10)
            .Select(i => new Server
            {
                Address = $"smtp.server{i}.ru",
                Login = $"Login-{i}",
                Password = $"Password-{i}",
                UseSSl = i % 2 == 0
            }).ToList();

        public static List<Message> Messages { get; } = Enumerable.Range(5, 30)
            .Select(i => new Message
            {
                Subject = $"Сообщение {i}",
                Body = $"Текст сообщения {i}"
            }).ToList();
    }
}
