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

        private void BtnLoginClick(object sender, RoutedEventArgs e)
        {
            //var client = new SmtpClient(cbServerSelect.Text, int.Parse(tbPortEdit.Text));
            //client.EnableSsl = true;
            

            //client.Credentials = new NetworkCredential
            //{
            //    UserName = tbLoginEdit.Text,
            //    SecurePassword = pbPasswordEdit.SecurePassword
            //};            
        }

        private void BtnSendClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
