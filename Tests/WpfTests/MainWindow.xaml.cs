using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfTests.Infrastructure.Extentions;

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

        async void OnOpenFileClick(object sender, RoutedEventArgs e)
        {
            //Мы находимся в ThreadId = 1
            await Task.Yield(); //Даем время на обработку сообщений UI
            //Мы снова в ThreadId = 1
                // --- Возвращает в основной поток только из основного потока; если изначально находились не в основном потоке, вернет в случайный поток из пула

            var dialog = new OpenFileDialog()
            {
                Title = "Выбор файла для чтения",
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                
            };

            if (dialog.ShowDialog() != true) return;

            StartAction.IsEnabled = false;
            CancelAction.IsEnabled = true;

            readingFileCancellation = new CancellationTokenSource();

            var cancel = readingFileCancellation.Token;
            IProgress<double> progress = new Progress<double>(p => ProgressInfo.Value = p);
            try
            {

                var count = await GetWordsCountAsync(dialog.FileName, progress, cancel);
                Result.Text = $"Word count {count}";
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("File reading operation was cancelled");
                Result.Text = "-----";
                progress.Report(0);
            }
            CancelAction.IsEnabled = false;
            StartAction.IsEnabled = true;
        }

        static async Task<int> GetWordsCountAsync(string fileName, IProgress<double> progress = null, CancellationToken cancel = default)
        {
            await Task.Yield().ConfigureAwait(false);

            var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            cancel.ThrowIfCancellationRequested();
            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    cancel.ThrowIfCancellationRequested();
                    var line = await reader.ReadLineAsync()
                        .ConfigureAwait(false); // если false - метод продолжается в случайном потоке из пула   
                                                // -- ConfigureAwait(false) обязательно использовать в библиотечных методах / технических методах, не работающих с интерфейсом --
                                                // если true (по умолчанию) - возвращается в главный поток
                    var words = line.Split(' ');
                    //Thread.Sleep (1000);
                    await Task.Delay(1, cancel).ConfigureAwait(false);

                    foreach (var word in words)
                        if (dict.ContainsKey(word))
                            dict[word]++;
                        else
                            dict.Add(word, 1);
                    progress?.Report(reader.BaseStream.Position / (double)reader.BaseStream.Length);
                }
            }

            return dict.Count;
        }

        CancellationTokenSource readingFileCancellation;

        private void OnCancelReadingClick(object sender, RoutedEventArgs e)
        {
            readingFileCancellation?.Cancel();
        }
    }
}
