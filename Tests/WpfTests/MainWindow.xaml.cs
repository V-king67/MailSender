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
                //Application.Current.Dispatcher.Invoke(() => TBComputingResult.Text = result);
                UpdateResultValue(result);
            })
            { IsBackground = true }.Start();
        }

        void UpdateResultValue(string result)
        {
            //Проверяем, находимся ли мы в одном потоке с интерфейсом (главном)
            if (Dispatcher.CheckAccess()) 
                TBComputingResult.Text = result;    //Алгоритм изменения интерфейса, который нужно исполнить
            //Если нет, запускаем этот метод в главном потоке через диспетчер
            else
                Dispatcher.Invoke(() => UpdateResultValue(result));
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
