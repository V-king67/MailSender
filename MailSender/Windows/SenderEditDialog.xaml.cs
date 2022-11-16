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
    /// Логика взаимодействия для SenderEditDialog.xaml
    /// </summary>
    public partial class SenderEditDialog : Window
    {
        public SenderEditDialog() => InitializeComponent();

        void OnButtonClick(object sender, RoutedEventArgs e)
        {
            //Если кнопка IsCancel == true, то результатом диалога будет false
            DialogResult = !((Button)e.OriginalSource).IsCancel;
            Close();
        }

        public static bool ShowDialog(string title, ref string name, ref string address)
        {
            var dialog = new SenderEditDialog
            {
                Title = title,
                SenderName = { Text = name },
                SenderAddress = { Text = address },
                Owner = Application.Current.Windows.Cast<Window>().FirstOrDefault(dialog => dialog.IsActive)
            };

            if(dialog.ShowDialog() != true) return false;

            name = dialog.SenderName.Text;
            address = dialog.SenderAddress.Text;
            return true;
        }

        public static bool Create(out string name, out string address)
        {
            name = null;
            address = null;

            return ShowDialog("Добавление отправителя", ref name, ref address);
        }
    }
}
