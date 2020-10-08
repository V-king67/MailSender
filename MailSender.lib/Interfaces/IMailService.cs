namespace MailSender.lib.Interfaces
{
    public interface IMailService
    {
        IMailSender GetSender(string address, int port, bool useSSL, string login, string password);
    }

    public interface IMailSender
    {
        void Send(string senderAddress, string recipientAddress, string subject, string body);
    }
}
