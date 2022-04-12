using System;

namespace MailSender.lib.Models
{
    public class Server
    {
        public string Name { get; set; }
        public string Address { get; set; }

        private int port = 25;
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
        public bool UseSSL { get; set; }
        public string Description { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
