using System.Collections.Generic;

namespace MailSender.lib.Interfaces
{
    public interface IStorage<T>
    {
        ICollection<T> Items { get; }

        void Load(ICollection<T> items);

        void SaveChanges();
    }
}
