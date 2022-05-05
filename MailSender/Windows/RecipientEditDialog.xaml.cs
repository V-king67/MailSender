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
    public partial class RecipientEditDialog : Window
    {
        public RecipientEditDialog() => InitializeComponent();

        void OnButtonClick(object sender, RoutedEventArgs e)
        {
            //Если кнопка IsCancel == true, то результатом диалога будет false
            DialogResult = !((Button)e.OriginalSource).IsCancel;
            Close();
        }

        public static bool ShowDialog(string title, ref string name, ref string address, ref string description)
        {
            var dialog = new RecipientEditDialog
            {
                Title = title,
                RecipientName = { Text = name },
                RecipientAddress = { Text = address },
                RecipientDescription = { Text = description },
                Owner = Application.Current.Windows.Cast<Window>().FirstOrDefault(dialog => dialog.IsActive)
            };

            if(dialog.ShowDialog() != true) return false;

            name = dialog.RecipientName.Text;
            address = dialog.RecipientAddress.Text;
            description = dialog.RecipientDescription.Text;
            return true;
        }

        public static bool Create(out string name, out string address, out string description)
        {
            name = null;
            address = null;
            description = null;

            return ShowDialog("Добавление получателя", ref name, ref address, ref description);
        }
    }
}
