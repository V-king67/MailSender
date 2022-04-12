using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.lib.Interfaces
{
    public interface IStorage<T>
    {
        ICollection<T> Items { get; }

        void Load(List<T> items);

        void SaveChanges();
    }
}
