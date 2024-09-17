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

        private void ComputeBtnClick(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                var result = GetResultHard();
                //Application.Current.Dispatcher.Invoke(() => TbResult.Text = result);
                UpdateResultValue(result);
            })
            { IsBackground = true }.Start();
        }

        void UpdateResultValue(string result)
        {
            if (Dispatcher.CheckAccess())
                TbResult.Text = result;
            else
                Dispatcher.Invoke(() => UpdateResultValue(result));
        }

        string GetResultHard()
        {
            for (int i = 0; i < 500; i++)
            {
                Thread.Sleep(10);
            }

            return "Hello world";
        }
    }
}
