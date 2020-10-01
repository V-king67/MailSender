using WpfTests.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows.Input;
using WpfTests.Infrastructure.Commands;
using System.Windows;

namespace WpfTests.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        string _title = "Тестовое окно";
        ICommand _showDialogCommand;

        public ICommand ShowDialogCommand => _showDialogCommand
            ??= new LambdaCommand(OnShowDialogCommandExecuted);

        private void OnShowDialogCommandExecuted(object p)
        {
            MessageBox.Show("Hello world!");
        }
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
            /*set
            {
                if (_title == value) return;
                _title = value;
                //OnPropertyChanged("Title");
                //OnPropertyChanged(nameof(Title));
                OnPropertyChanged();
            }*/
        }

        public DateTime CurrentTime => DateTime.Now;
        private readonly Timer _timer;
        private bool _timerEnabled = true;
        public bool TimerEnabled
        {
            get => _timerEnabled;
            set
            {
                if (!Set(ref _timerEnabled, value)) return;
                _timer.Enabled = value;
            }
        }

        public MainWindowViewModel()
        {
            _timer = new Timer(100);
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentTime));
        }
    }
}
