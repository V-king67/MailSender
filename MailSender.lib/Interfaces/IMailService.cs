using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

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
        Task SendAsync(string senderAddress, string recipientAddress, string subject, string body);
        Task SendAsync(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body, 
            IProgress<(string recipient, double percent)> progress = null, CancellationToken cancel = default);
        Task SendParallelAsync(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body, 
            CancellationToken cancel = default);
    }
}
