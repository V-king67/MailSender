using System.Windows.Controls;

namespace MailSender.Views
{
    public partial class RecipientEditor : UserControl
    {
        public RecipientEditor()
        {
            InitializeComponent();
        }

        private void OnDataValidationError(object sender, ValidationErrorEventArgs e)
        {
            //var control = (Control)e.OriginalSource;
            //if (e.Action == ValidationErrorEventAction.Added)
            //    control.ToolTip = e.Error.ErrorContent.ToString();
            //else control.ClearValue(ToolTipProperty);
        }
    }
}
