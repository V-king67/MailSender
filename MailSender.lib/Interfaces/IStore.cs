using MailSender.lib.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.lib.Interfaces
{
    public interface IStore<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}
