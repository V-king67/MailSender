using MailSender.lib;
using MailSender.Models;
using System.Net.Mail;
using System.Windows;

namespace MailSender
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendButtonClick(object sender, RoutedEventArgs e)
        {
            var mailSender = cbSendersList.SelectedItem as Sender;
            if (mailSender is null) return;
            if (!(dgRecipientsList.SelectedItem is Recipient recipient)) return;
            if (!(cbServersList.SelectedItem is Server server)) return;
            if (!(lbMessagesList.SelectedItem is Message message)) return;

            var sendService = new MailSenderService
            {
                ServerAddress = server.Address,
                ServerPort = server.Port,
                UseSSL = server.UseSSl,
                Login = server.Login,
                Password = server.Password
            };

            try
            {
                sendService.SendMessage(mailSender.Address, recipient.Address, message.Subject, message.Body);
            }
            catch(SmtpException error)
            {
                MessageBox.Show("Ошибка при отправке почты " + error.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
