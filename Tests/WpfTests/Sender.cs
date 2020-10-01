using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace WpfTests
{
    public static class Sender
    {
        public static SmtpClient Client { get; set; }
        public static MailAddress Address { get; set; }
    }
}
