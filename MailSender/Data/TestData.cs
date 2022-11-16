using MailSender.lib.Models;
using MailSender.lib.Service;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace MailSender.Data
{
    public class TestData
    {
        public List<Sender> Senders { get; set; }

        public List<Recipient> Recipients { get; set; }

        public List<Server> Servers { get; set; }

        public List<Message> Messages { get; set; }

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

        public TestData TestInitialize()
        {
            return new TestData
            {
                Senders = new List<Sender>(
                    Enumerable.Range(1, 10)
                    .Select(i => new Sender
                    {
                        Name = $"Отправитель {i}",
                        Address = $"Sender_{i}@server.ru"
                    }).ToList()),

                Recipients = new List<Recipient>(
                    Enumerable.Range(1, 10)
                    .Select(i => new Recipient
                    {
                        Name = $"Получатель {i}",
                        Address = $"Recipient_{i}@server.ru"
                    }).ToList()),

                Servers = new List<Server>(
                    Enumerable.Range(1, 10)
                    .Select(i => new Server
                    {
                        Address = $"smtp.server{i}.ru",
                        Login = $"Login-{i}",
                        Password = TextEncoder.Encode($"Password-{i}"),
                        UseSSL = i % 2 == 0
                    }).ToList()),

                Messages = new List<Message>(
                    Enumerable.Range(5, 30)
                    .Select(i => new Message
                    {
                        Subject = $"Сообщение {i}",
                        Body = $"Текст сообщения {i}"
                    }).ToList())
            };
        }
    }
}
