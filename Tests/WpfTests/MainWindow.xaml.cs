using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSendClick(object sender, RoutedEventArgs e)
        {
            var to = new MailAddress("jaskev@yandex.ru", "Иван");
            var from = new MailAddress("jaskev@yandex.ru", "Иван");
            var message = new MailMessage(from, to);

            message.Subject = "Заголовок письма от " + DateTime.Now;
            message.Body = "Текст тестового письма";

            var client = new SmtpClient("smtp.yandex.ru", 25);
            client.EnableSsl = true;
            

            client.Credentials = new NetworkCredential
            {
                UserName = tbLoginEdit.Text,
                SecurePassword = pbPasswordEdit.SecurePassword
            };

            client.Send(message);
            
        }
    }
}
