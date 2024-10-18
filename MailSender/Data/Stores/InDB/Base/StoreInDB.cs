using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using MailSender.lib.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.Stores.InDB.Base
{
    internal class StoreInDB<T> : IStore<T> where T : Entity
    {
        private readonly MailsenderDB db;
        private readonly DbSet<T> set;

        public StoreInDB(MailsenderDB db)
        {
            this.db = db;
            set = db.Set<T>();
        }

        public T Add(T item)
        {
            //set.Add(item);
            db.Entry(item).State = EntityState.Added;
            db.SaveChanges();
            return item;
        }

        public void Delete(int id)
        {
            var item = GetById(id);
            if (item is null) return;

            //set.Remove(item);
            db.Entry(item).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public IEnumerable<T> GetAll() => set.ToArray();

        //public T GetById(int id) => set.FirstOrDefault(x => x.Id == id);
        //public T GetById(int id) => set.SingleOrDefault(r => r.Id == id);
        public T GetById(int id) => set.Find(id);

        public void Update(T item)
        {
            //set.Update(item);
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
