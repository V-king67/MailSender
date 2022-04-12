using MailSender.lib.Interfaces.Models;

namespace MailSender.Models
{
    public class Sender : ISender
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
