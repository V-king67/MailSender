using MailSender.Infrastructure.Commands.Base;
using System.Linq;
using System.Windows;

namespace MailSender.Infrastructure.Commands
{
    class CloseWindowCommand : Command
    {
        protected override void Execute(object p)
        {
            var window = p as Window;

            if (window is null) window = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);
            if (window is null) window = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);
            window?.Close();
        }
    }
}
