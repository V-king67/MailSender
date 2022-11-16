using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MailSender.lib.Service
{
    public class DataStorageInXmlFile : IStorage<Message>, IStorage<Recipient>, IStorage<Sender>, IStorage<Server>
    {
        public class DataStructure
        {
            public List<Message> Messages { get; set; } = new List<Message>();
            public List<Recipient> Recipients { get; set; } = new List<Recipient>();
            public List<Sender> Senders { get; set; } = new List<Sender>();
            public List<Server> Servers { get; set; } = new List<Server>();
        }

        readonly string _FileName;

        public DataStorageInXmlFile(string fileName) => _FileName = fileName;

        DataStructure Data { get; set; } = new DataStructure();

        ICollection<Message> IStorage<Message>.Items => Data.Messages;
        ICollection<Recipient> IStorage<Recipient>.Items => Data.Recipients;
        ICollection<Sender> IStorage<Sender>.Items => Data.Senders;
        ICollection<Server> IStorage<Server>.Items => Data.Servers;

        public void Load()
        {
            if (!File.Exists(_FileName))
            {
                Data = new DataStructure();
                return;
            }

            using var file = File.OpenText(_FileName);
            if (file.BaseStream.Length == 0)
            {
                Data = new DataStructure();
                return;
            }

            var serializer = new XmlSerializer(typeof(DataStructure));
            Data = (DataStructure)serializer.Deserialize(file);
        }

        public void SaveChanges()
        {
            using var file = File.CreateText(_FileName);
            var serializer = new XmlSerializer(typeof(DataStructure));
            serializer.Serialize(file, Data);
        }
    }
}
