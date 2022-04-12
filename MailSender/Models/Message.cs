using MailSender.lib.Interfaces.Models;

namespace MailSender.Models
{
    public class Message : IMessage
    {
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
