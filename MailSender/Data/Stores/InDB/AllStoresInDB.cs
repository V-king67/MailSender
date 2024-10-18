using MailSender.Data.Stores.InDB.Base;
using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.Stores.InDB
{
    internal class RecipientsStoreInDB(MailsenderDB db) : StoreInDB<Recipient>(db) { }
    internal class SendersStoreInDB(MailsenderDB db) : StoreInDB<Sender>(db) { }
    internal class ServersStoreInDB(MailsenderDB db) : StoreInDB<Server>(db) { }
    internal class MessagesStoreInDB(MailsenderDB db) : StoreInDB<Message>(db) { }
    internal class SchedulerTasksStoreInDB(MailsenderDB db) : StoreInDB<SchedulerTask>(db) { }

}
