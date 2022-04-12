using MailSender.lib.Interfaces;
using MailSender.lib.Interfaces.Models;
using System.Collections.Generic;

namespace MailSender.lib.Service
{
    public class DataStorageInMemory : IStorage<IMessage>, IStorage<IRecipient>, IStorage<ISender>, IStorage<IServer>
    {
        public ICollection<IMessage> Messages { get; set; }
        public ICollection<IRecipient> Recipients { get; set; }
        public ICollection<ISender> Senders { get; set; }
        public ICollection<IServer> Servers { get; set; }

        ICollection<IMessage> IStorage<IMessage>.Items => Messages;

        ICollection<IRecipient> IStorage<IRecipient>.Items => Recipients;

        ICollection<ISender> IStorage<ISender>.Items => Senders;

        ICollection<IServer> IStorage<IServer>.Items => Servers;

        public void Load()
        {
            throw new System.NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}
