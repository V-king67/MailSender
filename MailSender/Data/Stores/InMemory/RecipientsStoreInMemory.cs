using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using MailSender.lib.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.Stores.InMemory
{
    internal class RecipientsStoreInMemory : IStore<Recipient>
    {
        public Recipient Add(Recipient item)
        {
            if (TestData.Recipients.Contains(item)) return item;

            item.Id = TestData.Recipients.DefaultIfEmpty().Max(x => x.Id) + 1;
            TestData.Recipients.Add(item);
            return item;
        }

        public void Delete(int id)
        {
            var item = GetById(id);
            if (item is null) return;
            TestData.Recipients.Remove(item);
        }

        public IEnumerable<Recipient> GetAll() => TestData.Recipients;

        public Recipient GetById(int id) => GetAll().FirstOrDefault(x => x.Id == id);

        public void Update(Recipient item) { }
    }
}
