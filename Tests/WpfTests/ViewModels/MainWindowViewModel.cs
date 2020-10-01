using WpfTests.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace WpfTests.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        string _title = "Тестовое окно";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
            //set
            //{
            //    if (_title == value) return;
            //    _title = value;
            //    //OnPropertyChanged("Title");
            //    //OnPropertyChanged(nameof(Title));
            //    OnPropertyChanged();
            //}
        }

        public DateTime CurrentTime => DateTime.Now;
        private readonly Timer _timer;

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
