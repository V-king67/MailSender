using MailSender.Data;
using MailSender.Infrastructure.Commands;
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
        string _title = "Тестовое окно";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        ObservableCollection<Server> _servers;
        ObservableCollection<Sender> _senders;
        ObservableCollection<Recipient> _recipients;
        ObservableCollection<Message> _messages;

        #region Свойства коллекций
        public ObservableCollection<Server> Servers
        {
            get => _servers;
            set => Set(ref _servers, value);
        }
        public ObservableCollection<Sender> Senders
        {
            get => _senders;
            set => Set(ref _senders, value);
        }
        public ObservableCollection<Recipient> Recipients
        {
            get => _recipients;
            set => Set(ref _recipients, value);
        }
        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set => Set(ref _messages, value);
        }
        #endregion

        public MainWindowViewModel()
        {
            Servers = new ObservableCollection<Server>(TestData.Servers);
            Senders = new ObservableCollection<Sender>(TestData.Senders);
            Recipients = new ObservableCollection<Recipient>(TestData.Recipients);
            Messages = new ObservableCollection<Message>(TestData.Messages);
        }

        Server _selectedServer;
        Sender _selectedSender;
        Recipient _selectedRecipient;
        Message _selectedMessage;

        #region Свойства выбранных элементов
        public Server SelectedServer
        {
            get => _selectedServer;
            set => Set(ref _selectedServer, value);
        }
        public Sender SelectedSender
        {
            get => _selectedSender;
            set => Set(ref _selectedSender, value);
        }
        public Recipient SelectedRecipient
        {
            get => _selectedRecipient;
            set => Set(ref _selectedRecipient, value);
        }
        public Message SelectedMessage
        {
            get => _selectedMessage;
            set => Set(ref _selectedMessage, value);
        }
        #endregion

        #region Команды

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

        #endregion
    }
}
