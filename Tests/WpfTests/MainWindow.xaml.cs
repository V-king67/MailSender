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

        private void ComputeButton_Click(object sender, RoutedEventArgs e)
        {
            /*var result = LengthyWork();
            TBComputingResult.Text = result;*/     //Выполнение в главном потоке заблокирует интерфейс

            //new Thread(() => TBComputingResult.Text = LengthyWork()).Start();     //При этом состояние интерфейса нельзя изменить во вторичном потоке

            //Выполняем вычисление во вторичном потоке, затем используем диспетчер для доступа к основному потоку
            new Thread(() =>
            {
                var result = LengthyWork();
                Application.Current.Dispatcher.Invoke(() => TBComputingResult.Text = result);
            })
            { IsBackground = true }.Start();
        }

        string LengthyWork()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(10);
            }

            return "Calculation complete";
        }
    }
}
