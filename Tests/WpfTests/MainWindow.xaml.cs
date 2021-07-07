using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
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

        private async void OnOpenFileClick(object sender, RoutedEventArgs e)
        {
            //ThreadId == 1 (MainThread)
            await Task.Yield(); //Waiting for UI
            //ThreadId == 1

            var dialog = new OpenFileDialog
            {
                Title = "Выберите файл для чтения",
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() != true) return;

            openFile.IsEnabled = false;
            cancelReading.IsEnabled = true;

            _readingFileCancellation = new CancellationTokenSource();
            var cancel = _readingFileCancellation.Token;
            IProgress<double> progress = new Progress<double>(p => progressInfo.Value = p);

            try
            {
                tbResult.Text = "Считаем слова...";
                var count = await GetWordsCountAsync(dialog.FileName, progress, cancel);
                tbResult.Text = $"Число слов {count}";
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("Операция чтения файла была отменена.");
                tbResult.Text = "---";
                progress.Report(0);
            }

            cancelReading.IsEnabled = false;
            openFile.IsEnabled = true;
        }

        private static async Task<int> GetWordsCountAsync(string fileName, IProgress<double> progress = null, CancellationToken cancel = default)
        {
            var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            cancel.ThrowIfCancellationRequested();
            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    cancel.ThrowIfCancellationRequested();
                    var line = await reader.ReadLineAsync().ConfigureAwait(false);
                    var words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    //Thread.Sleep(100);
                    await Task.Delay(100);

                    foreach (var word in words)
                        if (dict.ContainsKey(word))
                            dict[word]++;
                        else dict.Add(word, 1);

                    progress?.Report(reader.BaseStream.Position / (double)reader.BaseStream.Length);
                }
            }
            return dict.Count;
        }

        private CancellationTokenSource _readingFileCancellation;
        private void OnCancelReadingClick(object sender, RoutedEventArgs e)
        {
            _readingFileCancellation?.Cancel();
        }



        //private void ComputeBtnClick(object sender, RoutedEventArgs e)
        //{
        //    new Thread(() =>
        //    {
        //        var result = GetResultHard();
        //        //Application.Current.Dispatcher.Invoke(() => TbResult.Text = result);
        //        UpdateResultValue(result);
        //    })
        //    { IsBackground = true }.Start();
        //}

        //void UpdateResultValue(string result)
        //{
        //    if (Dispatcher.CheckAccess())
        //        TbResult.Text = result;
        //    else
        //        Dispatcher.Invoke(() => UpdateResultValue(result));
        //}

        //string GetResultHard()
        //{
        //    for (int i = 0; i < 500; i++)
        //    {
        //        Thread.Sleep(10);
        //    }

        //    return "Hello world";
        //}
    }
}
