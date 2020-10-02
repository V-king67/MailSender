using MailSender.Data;
using MailSender.Models;
using MailSender.ViewModels.Base;
using System.Collections.ObjectModel;

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
    }
}
