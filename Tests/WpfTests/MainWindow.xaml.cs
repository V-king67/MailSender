using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;

namespace WpfTests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string LengthyWork()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(10);
            }

            return "Calculation complete";
        }

        private void OnOpenFileClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Title = "Выбор файла для чтения",
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                
            };

            if (dialog.ShowDialog() != true) return;

            var dict = new Dictionary<string, int> (StringComparer.OrdinalIgnoreCase);

            using (var reader = new StreamReader(dialog.FileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var words = line.Split(' ');
                    Thread.Sleep (1000);

                    foreach (var word in words)
                        if (dict.ContainsKey(word))
                            dict[word]++;
                        else
                            dict.Add(word, 1);
                }
            }

            Result.Text = $"Word count {dict.Count}";
        }
    }
}
