using MailSender.lib.Interfaces.Models;

namespace MailSender.Models
{
    public class Recipient : IRecipient
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
