using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Models
{
    class Server
    {
        public string Address { get; set; }

        private int port;
        public int Port
        {
            get => port;
            set
            {
                if (value < 0 || value >= 65535)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Номер порта должен быть в диапазоне от 0 до 65534");
                port = value;
            }
        }
        public bool UseSSl { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
