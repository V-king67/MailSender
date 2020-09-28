using System;
using System.Net;
using System.Net.Mail;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var to = new MailAddress("jaskev@yandex.ru", "Иван");
            var from = new MailAddress("jaskev@yandex.ru", "Иван");
            var message = new MailMessage(from, to);

            message.Subject = "Заголовок письма от " + DateTime.Now;
            message.Body = "Текст тестового письма";

            var client = new SmtpClient("smtp.yandex.ru", 587);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential
            {
                UserName = "user_name",
                Password = "Password"
            };

            client.Send(message);

            Console.WriteLine("Hello World!");
        }
    }
}
