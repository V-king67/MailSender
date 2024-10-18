using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data
{
    internal class MailSenderDBInitializer
    {
        private readonly MailsenderDB db;

        public MailSenderDBInitializer(MailsenderDB db) => this.db = db;

        public void Initialize()
        {
            db.Database.Migrate();

            InitializeRecipients();
            InitializeServers();
            InitializeSenders();
            InitializeMessages();
        }

        private void InitializeRecipients()
        {
            if (db.Recipients.Any()) return;

            db.Recipients.AddRange(TestData.Recipients);
            db.SaveChanges();
        }
        private void InitializeServers()
        {
            if (db.Servers.Any()) return;

            db.Servers.AddRange(TestData.Servers);
            db.SaveChanges();
        }
        private void InitializeSenders()
        {
            if (db.Senders.Any()) return;

            db.Senders.AddRange(TestData.Senders);
            db.SaveChanges();
        }
        private void InitializeMessages()
        {
            if (db.Messages.Any()) return;

            db.Messages.AddRange(TestData.Messages);
            db.SaveChanges();
        }
    }
}
