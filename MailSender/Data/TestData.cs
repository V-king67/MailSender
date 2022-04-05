using MailSender.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MailSender.lib.Service;
using System.Xml.Serialization;
using System.IO;

namespace MailSender.Data
{
    public class TestData
    {
        public List<Sender> Senders { get; set; } = new List<Sender>(
            Enumerable.Range(1, 10)
            .Select(i => new Sender
            {
                Name = $"Отправитель {i}",
                Address = $"Sender_{i}@server.ru"
            }).ToList());

        public List<Recipient> Recipients { get; set; } = new List<Recipient>(
            Enumerable.Range(1, 10)
            .Select(i => new Recipient
            {
                Name = $"Получатель {i}",
                Address = $"Recipient_{i}@server.ru"
            }).ToList());

        public List<Server> Servers { get; set; } = new List<Server>(
            Enumerable.Range(1, 10)
            .Select(i => new Server
            {
                Address = $"smtp.server{i}.ru",
                Login = $"Login-{i}",
                Password = TextEncoder.Encode($"Password-{i}"),
                UseSSl = i % 2 == 0
            }).ToList());

        public List<Message> Messages { get; set; } = new List<Message>(
            Enumerable.Range(5, 30)
            .Select(i => new Message
            {
                Subject = $"Сообщение {i}",
                Body = $"Текст сообщения {i}"
            }).ToList());

        public static TestData LoadFromXML(string fileName)
        {
            var serializer = new XmlSerializer(typeof(TestData));
            using var file = File.OpenText(fileName);
            var result = (TestData)serializer.Deserialize(file);
            return result;
        }
        public void SaveToXML(string fileName)
        {
            var serializer = new XmlSerializer(typeof(TestData));
            using var file = File.Create(fileName);
            serializer.Serialize(file, this);
        }
    }
}
