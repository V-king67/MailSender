using MailSender.lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.lib.Service
{
    public class DebugMailService : IMailService
    {
        private readonly IEncryptorService _encryptorService;

        public DebugMailService(IEncryptorService encryptorService)
        {
            _encryptorService = encryptorService;
        }
        public IMailSender GetSender(string address, int port, bool useSSL, string login, string password)
        {
            return new DebugMailSender(address, port, useSSL, login, password);
        }

        class DebugMailSender : IMailSender
        {
            private readonly string _address;
            private readonly int _port;
            private readonly bool _useSSL;
            private readonly string _login;
            private readonly string _password;

            public DebugMailSender(string address, int port, bool useSSL, string login, string password)
            {
                _address = address;
                _port = port;
                _useSSL = useSSL;
                _login = login;
                _password = password;
            }

            public void Send(string senderAddress, string recipientAddress, string subject, string body)
            {
                Debug.WriteLine("Отправка почты через сервер {0}:{1} SSL:{2} (Login:{3}; Password: {4}",
                    _address, _port, _useSSL, _login, _password);
                Debug.WriteLine("Сообщение от {0} к {1}:\r\n{2}\r\n{3}", senderAddress, recipientAddress, subject, body);
            }

            public void Send(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body)
            {
                foreach (var recipientAddress in recipientAddresses)
                    Send(senderAddress, recipientAddress, subject, body);
            }

            public Task SendAsync(string senderAddress, string recipientAddress, string subject, string body)
            {
                throw new NotImplementedException();
            }

            public Task SendAsync(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body, IProgress<(string recipient, double percent)> progress = null, CancellationToken cancel = default)
            {
                throw new NotImplementedException();
            }

            public void SendParallel(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body)
            {
                    Send(senderAddress, recipientAddresses, subject, body);
            }

            public Task SendParallelAsync(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body, CancellationToken cancel = default)
            {
                throw new NotImplementedException();
            }
        }
    }


}
