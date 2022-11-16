using MailSender.ViewModels.Base;

namespace MailSender.ViewModels
{
    class StatisticViewModel : ViewModel
    {
        #region SendMessagesCount
        private int _sendMessagesCount;
        public int SendMessagesCount { get => _sendMessagesCount; private set => Set(ref _sendMessagesCount, value); }
        #endregion

        public void MessageSent() => SendMessagesCount++;
    }
}
