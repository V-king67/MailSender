using System.Collections.Generic;

namespace MailSender.lib.Interfaces
{
    public interface IMailService
    {
        IMailSender GetSender(string address, int port, bool useSSL, string login, string password);
    }

    public interface IMailSender
    {
        void Send(string senderAddress, string recipientAddress, string subject, string body);
        void Send(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body);
        void SendParallel(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body);
    }
}
