using MailSender.Data;
using MailSender.Infrastructure.Commands;
using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using MailSender.ViewModels.Base;
using MailSender.Windows;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MailSender.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        readonly IMailService _mailService;
        readonly IStorage<Server> _ServerStorage;
        readonly IStorage<Sender> _SenderStorage;
        readonly IStorage<Recipient> _RecipientStorage;
        readonly IStorage<Message> _MessageStorage;

        public StatisticViewModel Statistic { get; } = new StatisticViewModel();

        string _title = "Тестовое окно";

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        public MainWindowViewModel(IMailService mailService, IStorage<Server> serverStorage, IStorage<Sender> senderStorage, IStorage<Recipient> recipientStorage, IStorage<Message> messageStorage)
        {
            _mailService = mailService;
            _ServerStorage = serverStorage;
            _SenderStorage = senderStorage;
            _RecipientStorage = recipientStorage;
            _MessageStorage = messageStorage;
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
            _ServerStorage.Load();
            _SenderStorage.Load();
            _RecipientStorage.Load();
            _MessageStorage.Load();

            Servers = new ObservableCollection<Server>(_ServerStorage.Items);
            Senders = new ObservableCollection<Sender>(_SenderStorage.Items);
            Recipients = new ObservableCollection<Recipient>(_RecipientStorage.Items);
            Messages = new ObservableCollection<Message>(_MessageStorage.Items);
        }
        #endregion

        #region SaveDataCommand
        ICommand _SaveDataCommand;
        public ICommand SaveDataCommand => _SaveDataCommand
            ??= new LambdaCommand(OnSaveDataCommandExecuted);

        private void OnSaveDataCommandExecuted(object p)
        {
            _ServerStorage.SaveChanges();
            _SenderStorage.SaveChanges();
            _RecipientStorage.SaveChanges();
            _MessageStorage.SaveChanges();
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

        #region ServerCommands

        #region CreateNewServerCommand
        ICommand _createNewServerCommand;
        public ICommand CreateNewServerCommand => _createNewServerCommand
            ??= new LambdaCommand(OnCreateNewServerCommandExecuted, CanCreateNewServerCommandExecute);

        private bool CanCreateNewServerCommandExecute(object p) => true;
        private void OnCreateNewServerCommandExecuted(object p)
        {
            if (!ServerEditDialog.Create(out var name, out var address, out var port,
                out var ssl, out var description, out var login, out var password))
                return;

            var server = new Server
            {
                Name = name,
                Address = address,
                Port = port,
                UseSSL = ssl,
                Description = description,
                Login = login,
                Password = password
            };

            _ServerStorage.Items.Add(server);
            Servers.Add(server);
        }
        #endregion

        #region EditServerCommand
        ICommand _editNewServerCommand;
        public ICommand EditServerCommand => _editNewServerCommand
            ??= new LambdaCommand(OnEditServerCommandExecuted, CanEditServerCommandExecute);

        private bool CanEditServerCommandExecute(object p) => p is Server || SelectedServer != null;
        private void OnEditServerCommandExecuted(object p)
        {
            //throw new NotImplementedException("Коллекция не реагирует на изменение элемента, т.е. визуально не изменяется в списке, дописать, например, списочный класс наследник");

            var server = p as Server ?? SelectedServer;
            if (server is null) return;

            var name = server.Name;
            var address = server.Address;
            var port = server.Port;
            var ssl = server.UseSSL;
            var description = server.Description;
            var login = server.Login;
            var password = server.Password;

            if (!ServerEditDialog.ShowDialog("Редактирование сервера", ref name, ref address,
                ref port, ref ssl, ref description, ref login, ref password))
                return;
            server.Name = name;
            server.Address = address;
            server.Port = port;
            server.UseSSL = ssl;
            server.Description = description;
            server.Login = login;
            server.Password = password;

            SelectedServer = server;
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
            _ServerStorage.Items.Remove(server);
            Servers.Remove(server);
            SelectedServer = Servers.FirstOrDefault();
        }
        #endregion

        #endregion

        #region SenderCommands

        #region CreateSenderCommand
        ICommand _CreateSenderCommand;
        public ICommand CreateSenderCommand => _CreateSenderCommand
            ??= new LambdaCommand(OnCreateSenderCommandExecuted);

        private void OnCreateSenderCommandExecuted(object p)
        {
            if(!SenderEditDialog.Create(out var name, out var address)) return;

            var sender = new Sender { Name = name, Address = address };
            _SenderStorage.Items.Add(sender);
            Senders.Add(sender);
        }
        #endregion

        #region EditSenderCommand
        ICommand _EditSenderCommand;
        public ICommand EditSenderCommand => _EditSenderCommand
            ??= new LambdaCommand(OnEditSenderCommandExecuted, CanEditSenderCommandExecute);

        private bool CanEditSenderCommandExecute(object p) => p is Sender || SelectedSender != null;

        private void OnEditSenderCommandExecuted(object p)
        {
            var sender = p as Sender ?? SelectedSender;
            if (sender is null) return;

            var name = sender.Name;
            var address = sender.Address;

            if (!SenderEditDialog.ShowDialog("Редактирование отправителя", ref name, ref address)) return;
            sender.Address = address;
            sender.Name = name;

            SelectedSender = sender;
        }
        #endregion

        #region DeleteSenderCommand
        ICommand _DeleteSenderCommand;
        public ICommand DeleteSenderCommand => _DeleteSenderCommand
            ??= new LambdaCommand(OnDeleteSenderCommandExecuted, CanDeleteSenderCommandExecute);

        private bool CanDeleteSenderCommandExecute(object p) => p is Sender || SelectedSender != null;

        private void OnDeleteSenderCommandExecuted(object p)
        {
            var sender = p as Sender ?? SelectedSender;
            if(sender is null) return;
            _SenderStorage.Items.Remove(sender);
            Senders.Remove(sender);
            SelectedSender = Senders.FirstOrDefault();
        }
        #endregion

        #endregion

        #region RecipientCommands

        #region CreateRecipientCommand
        ICommand _CreateRecipientCommand;
        public ICommand CreateRecipientCommand => _CreateRecipientCommand
            ??= new LambdaCommand(OnCreateRecipientCommandExecuted);

        private void OnCreateRecipientCommandExecuted(object p)
        {
            if (!RecipientEditDialog.Create(out var name, out var address, out var description)) return;

            var recipient = new Recipient { Name = name, Address = address, Description = description };
            _RecipientStorage.Items.Add(recipient);
            Recipients.Add(recipient);
        }
        #endregion

        #region EditRecipientCommand
        ICommand _EditRecipientCommand;
        public ICommand EditRecipientCommand => _EditRecipientCommand
            ??= new LambdaCommand(OnEditRecipientCommandExecuted, CanEditRecipientCommandExecute);

        private bool CanEditRecipientCommandExecute(object p) => p is Recipient || SelectedRecipient != null;

        private void OnEditRecipientCommandExecuted(object p)
        {
            var recipient = p as Recipient ?? SelectedRecipient;
            if (recipient is null) return;

            var name = recipient.Name;
            var address = recipient.Address;
            var description = recipient.Description;

            if (!RecipientEditDialog.ShowDialog("Редактирование получателя", ref name, ref address, ref description)) return;
            recipient.Address = address;
            recipient.Name = name;
            recipient.Description = description;

            SelectedRecipient = recipient;
        }
        #endregion

        #region DeleteRecipientCommand
        ICommand _DeleteRecipientCommand;
        public ICommand DeleteRecipientCommand => _DeleteRecipientCommand
            ??= new LambdaCommand(OnDeleteRecipientCommandExecuted, CanDeleteRecipientCommandExecute);

        private bool CanDeleteRecipientCommandExecute(object p) => p is Recipient || SelectedRecipient != null;

        private void OnDeleteRecipientCommandExecuted(object p)
        {
            var recipient = p as Recipient ?? SelectedRecipient;
            if (recipient is null) return;
            _RecipientStorage.Items.Remove(recipient);
            Recipients.Remove(recipient);
            SelectedRecipient = Recipients.FirstOrDefault();
        }
        #endregion

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

            var mailSender = _mailService.GetSender(server.Address, server.Port, server.UseSSL, server.Login, server.Password);
            mailSender.Send(sender.Address, recipient.Address, message.Subject, message.Body);

            Statistic.MessageSent();
        }
        #endregion

        #endregion
    }
}
