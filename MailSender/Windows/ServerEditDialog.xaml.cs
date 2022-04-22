using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MailSender.Windows
{
    /// <summary>
    /// Логика взаимодействия для ServerEditDialog.xaml
    /// </summary>
    public partial class ServerEditDialog : Window
    {
        ServerEditDialog() => InitializeComponent();

        void OnPortTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(sender is TextBox textBox) || textBox.Text == "") return;
            e.Handled = !int.TryParse(textBox.Text, out _);
        }

        void OnButtonClick(object sender, RoutedEventArgs e)
        {
            //Если кнопка IsCancel == true, то результатом диалога будет false
            DialogResult = !((Button)e.OriginalSource).IsCancel;
            Close();
        }

        public static bool ShowDialog(string title, ref string name, ref string address, ref int port,
            ref bool useSSL, ref string description, ref string login, ref string password)
        {
            var window = new ServerEditDialog
            {
                Title = title,
                ServerName = { Text = name },
                ServerAddress = { Text = address },
                ServerPort = { Text = port.ToString() },
                ServerSSL = { IsChecked = useSSL },
                Login = { Text = login },
                Password = { Password = password },
                ServerDescription = { Text = description },
                Owner = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window.IsActive)
            };

            //Если получен Cancel - параметры не изменяются
            if (window.ShowDialog() != true) return false;

            //Если OK
            name = window.ServerName.Text;
            address = window.ServerAddress.Text;
            port = int.Parse(window.ServerPort.Text);
            useSSL = (bool)window.ServerSSL.IsChecked;
            description = window.ServerDescription.Text;
            login = window.Login.Text;
            password = window.Password.Password;
            return true;
        }

        /// <summary>
        /// Метод, позволяющий отобразить диалог создания нового сервера
        /// Редактируемые параметры передаются по ссылке
        /// Если пользователь выбрал Ok, то метод возвращает true
        /// Если пользователь выбрал Cancel, то возвращаются дефолтные значения
        /// </summary>
        public static bool Create(
            out string name,
            out string address,
            out int port,
            out bool useSSL,
            out string description,
            out string login,
            out string password)
        {
            // Инициализируем переменные значениями на случай отмены операции
            name = null;
            address = null;
            port = 25;
            useSSL = false;
            description = null;
            login = null;
            password = null;

            return ShowDialog("Создать сервер", ref name, ref address, ref port,
                ref useSSL, ref description, ref login, ref password);
        }

    }
}
