using MailSender.Infrastructure.Commands;
using MailSender.lib.Models;
using MailSender.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MailSender.ViewModels
{
    internal class RecipientEditWindowViewModel : ViewModel
    {
        Recipient _Recipient;

        public Recipient Recipient
        {
            get => _Recipient;
            set => Set(ref _Recipient, value);
        }

        public RecipientEditWindowViewModel(Recipient recipient = null)
        {
            if (Recipient != null) Recipient = recipient;
        }


        #region SaveCommand
        ICommand _SaveCommand;
        public ICommand SaveCommand => _SaveCommand
            ??= new LambdaCommand(OnSaveCommandExecuted);

        private void OnSaveCommandExecuted(object p)
        {
            
        }
        #endregion

    }
}
