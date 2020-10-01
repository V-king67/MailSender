using WpfTests.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfTests.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        string _title = "Тестовое окно";
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value) return;
                _title = value;
                OnPropertyChanged("Title");
            }
        }
    }
}
