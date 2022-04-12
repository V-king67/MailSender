namespace MailSender.lib.Interfaces.Models
{
    public interface IMessage
    {
        string Subject { get; set; }
        string Body { get; set; }
    }
}
