using MailSender.lib.Models.Base;
using System;
using System.Collections.Generic;

namespace MailSender.lib.Models
{
    public class SchedulerTask : Entity
    {
        public DateTime Time { get; set; }
        public Server Server { get; set; }
        public Sender Sender { get; set; }
        public ICollection<Recipient> Recipients { get; set; }
        public Message Message {  get; set; }
    }
}
