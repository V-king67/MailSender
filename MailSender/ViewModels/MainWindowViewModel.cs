using MailSender.Data;
using MailSender.Infrastructure.Commands;
using MailSender.lib.Interfaces;
using MailSender.Models;
using MailSender.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MailSender.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        readonly IMailService _mailService;

        public StatisticViewModel Statistic { get; } = new StatisticViewModel();

        string _title = "Тестовое окно";

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        public MainWindowViewModel(IMailService mailService)
        {
            _mailService = mailService;
        }

        #region Коллекции

        ObservableCollection<Server> _servers;
        public ObservableCollection<Server> Servers
        {
            get => _servers;
            set => Set(ref _servers, value);
        }

        ObservableCollection<Sender> _senders;
        public ObservableCollection<Sender> Senders
        {
            get => _senders;
            set => Set(ref _senders, value);
        }

        ObservableCollection<Recipient> _recipients;
        public ObservableCollection<Recipient> Recipients
        {
            get => _recipients;
            set => Set(ref _recipients, value);
        }

        ObservableCollection<Message> _messages;
        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set => Set(ref _messages, value);
        }
        #endregion

        #region Выбранные элементы

        Server _selectedServer;
        public Server SelectedServer
        {
            get => _selectedServer;
            set => Set(ref _selectedServer, value);
        }

        Sender _selectedSender;
        public Sender SelectedSender
        {
            get => _selectedSender;
            set => Set(ref _selectedSender, value);
        }

        Recipient _selectedRecipient;
        public Recipient SelectedRecipient
        {
            get => _selectedRecipient;
            set => Set(ref _selectedRecipient, value);
        }

        Message _selectedMessage;
        public Message SelectedMessage
        {
            get => _selectedMessage;
            set => Set(ref _selectedMessage, value);
        }
        #endregion

        #region Команды

        #region LoadDataCommand
        ICommand _LoadDataCommand;
        public ICommand LoadDataCommand => _LoadDataCommand
            ??= new LambdaCommand(OnLoadDataCommandExecuted);

        private void OnLoadDataCommandExecuted(object p)
        {
            Servers = new ObservableCollection<Server>(TestData.Servers);
            Senders = new ObservableCollection<Sender>(TestData.Senders);
            Recipients = new ObservableCollection<Recipient>(TestData.Recipients);
            Messages = new ObservableCollection<Message>(TestData.Messages);
        }
        #endregion

        #region ShowAboutCommand
        ICommand _ShowAboutCommand;
        public ICommand ShowAboutCommand => _ShowAboutCommand
            ??= new LambdaCommand(OnShowAboutCommandExecuted);

        private void OnShowAboutCommandExecuted(object p)
        {
            MessageBox.Show("Программа отправки почты\nАвтор: Иван Яскевич", 
                            "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region CreateNewServerCommand
        ICommand _createNewServerCommand;
        public ICommand CreateNewServerCommand => _createNewServerCommand
            ??= new LambdaCommand(OnCreateNewServerCommandExecuted, CanCreateNewServerCommandExecute);

        private bool CanCreateNewServerCommandExecute(object p) => true;
        private void OnCreateNewServerCommandExecuted(object p)
        {
            MessageBox.Show("Создание нового сервера", "Управление серверами");
        }
        #endregion

        #region EditServerCommand
        ICommand _editNewServerCommand;
        public ICommand EditServerCommand => _editNewServerCommand
            ??= new LambdaCommand(OnEditServerCommandExecuted, CanEditServerCommandExecute);

        private bool CanEditServerCommandExecute(object p) => p is Server || SelectedServer != null;
        private void OnEditServerCommandExecuted(object p)
        {
            var server = p as Server ?? SelectedServer;
            if (server is null) return;
            MessageBox.Show($"Редактирование сервера {server.Address}", "Управление серверами");
        }
        #endregion

        #region DeleteServerCommand
        ICommand _deleteServerCommand;
        public ICommand DeleteServerCommand => _deleteServerCommand
            ??= new LambdaCommand(OnDeleteServerCommandExecuted, CanDeleteServerCommandExecute);

        private bool CanDeleteServerCommandExecute(object p) => p is Server || SelectedServer != null;
        private void OnDeleteServerCommandExecuted(object p)
        {
            var server = p as Server ?? SelectedServer;
            if (server is null) return;
            Servers.Remove(server);
            SelectedServer = Servers.FirstOrDefault();
            //MessageBox.Show($"Удаление сервера {server.Address}", "Управление серверами");
        }
        #endregion

        #region SendMailCommand
        ICommand _sendMailCommand;
        public ICommand SendMailCommand => _sendMailCommand
            ??= new LambdaCommand(OnSendMailCommandExecuted, CanSendMailCommandExecute);

        private bool CanSendMailCommandExecute(object p)
        {
            if (SelectedServer is null || SelectedSender is null || SelectedRecipient is null || SelectedMessage is null) return false;
            return true;
        }
        private void OnSendMailCommandExecuted(object p)
        {
            var server = SelectedServer;
            var sender = SelectedSender;
            var recipient = SelectedRecipient;
            var message = SelectedMessage;

            var mailSender = _mailService.GetSender(server.Address, server.Port, server.UseSSl, server.Login, server.Password);
            mailSender.Send(sender.Address, recipient.Address, message.Subject, message.Body);

            Statistic.MessageSent();
        }
        #endregion

        #endregion
    }
}
