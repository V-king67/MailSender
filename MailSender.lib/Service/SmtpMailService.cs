using MailSender.lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.lib.Service
{
    public class SmtpMailService : IMailService
    {
        public SmtpMailService()
        {

        }

        public IMailSender GetSender(string address, int port, bool useSSL, string login, string password)
        {
            return new SmtpMailSender(address, port, useSSL, login, password);
        }
    }

    internal class SmtpMailSender : IMailSender
    {
        private readonly string _address;
        private readonly int _port;
        private readonly bool _useSSL;
        private readonly string _login;
        private readonly string _password;

        public SmtpMailSender(string address, int port, bool useSSL, string login, string password)
        {
            _address = address;
            _port = port;
            _useSSL = useSSL;
            _login = login;
            _password = password;
        }

        public void Send(string senderAddress, string recipientAddress, string subject, string body)
        {
            var from = new MailAddress(senderAddress);
            var to = new MailAddress(recipientAddress);
            using var message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;

            using var client = new SmtpClient(_address, _port);
            client.EnableSsl = _useSSL;

            client.Credentials = new NetworkCredential
            {
                UserName = _login,
                Password = _password
            };

            try
            {
                client.Send(message);
            }
            catch (SmtpException e)
            {
                Trace.TraceError(e.ToString());
                throw;
            }
        }

        public void Send(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body)
        {
            foreach (var recipientAddress in recipientAddresses)
                Send(senderAddress, recipientAddress, subject, body);
        }

        public void SendParallel(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body)
        {
            foreach (var recipientAddress in recipientAddresses)
                ThreadPool.QueueUserWorkItem(o => Send(senderAddress, recipientAddress, subject, body));
        }

        public async Task SendAsync(
            string senderAddress, string recipientAddress, string subject, string body)
        {
            var from = new MailAddress(senderAddress);
            var to = new MailAddress(recipientAddress);
            using var message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;

            using var client = new SmtpClient(_address, _port);
            client.EnableSsl = _useSSL;

            client.Credentials = new NetworkCredential
            {
                UserName = _login,
                Password = _password
            };

            try
            {
                await client.SendMailAsync(message).ConfigureAwait(false);
            }
            catch (SmtpException e)
            {
                Trace.TraceError(e.ToString());
                throw;
            }
        }

        public async Task SendAsync(
            string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body, 
            IProgress<(string recipient, double percent)> progress = null, CancellationToken cancel = default)
        {
            var recipients = recipientAddresses.ToArray();

            for (int i = 0, count = recipients.Length; i < count; i++) 
            {
                cancel.ThrowIfCancellationRequested();
                await SendAsync(senderAddress, recipients[i], subject, body).ConfigureAwait(false);
                progress?.Report((recipients[i], i / (double)count));
            }
        }

        public async Task SendParallelAsync(
            string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body, 
            CancellationToken cancel = default)
        {
            var tasks = recipientAddresses.Select(
                recipientAdress => SendAsync(senderAddress, recipientAdress, subject, body));
            
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}
