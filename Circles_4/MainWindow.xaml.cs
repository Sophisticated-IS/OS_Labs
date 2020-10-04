using System;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Circles_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Thread[] _threads = new Thread[3];
        public string[] Priorities { get; set; }

        /// <summary>
        /// Лабораторная работа по Защите ОС задания №4
        /// Автор: Сова Игорь КМБ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitThreads();
            InitPriorities();
        }

        private void InitPriorities()
        {
            firstCircle.ItemsSource = GetPriorities();
            secondCircle.ItemsSource = GetPriorities();
            thirdCircle.ItemsSource = GetPriorities();
            firstCircle.SelectedIndex = 2;
            secondCircle.SelectedIndex = 2;
            thirdCircle.SelectedIndex = 2;
        }

        private string[] GetPriorities()
        {
            return Enum.GetNames(typeof(ThreadPriority)).ToArray();
        }

        private void InitThreads()
        {
            _threads[0] = new Thread(delegate() { DrawCircle(10, 10, Canvas, Brushes.Brown); });

            _threads[1] = new Thread(delegate() { DrawCircle(10, 10, Canvas, Brushes.Yellow); });

            _threads[2] = new Thread(delegate() { DrawCircle(10, 10, Canvas, Brushes.Aqua); });
        }

        private void StartThreads()
        {
            foreach (var thread in _threads)
            {
                thread.Start();
            }
        }

        private void BtnStartStop_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_threads[0].IsAlive)
            {
                StartThreads();
            }
        }

        private void DrawCircle(int width, int height, Canvas cv, SolidColorBrush brush)
        {
            while (true)
            {
                Thread.Sleep(100);

                //Добавляем случайности передавая в конструктор число из криптостойкого алгоритма
                var random = new Random(GetCryptoRandomNumber());
                var x = random.Next(0, (int) cv.ActualWidth);
                var y = random.Next(0, (int) cv.ActualHeight);
                
                Dispatcher.Invoke(() =>
                {
                    var circle = new Ellipse
                    {
                        Width = width,
                        Height = height,
                        StrokeThickness = 6,
                        Stroke = brush
                    };
                    cv.Children.Add(circle);

                    circle.SetValue(Canvas.LeftProperty, (double) x);
                    circle.SetValue(Canvas.TopProperty, (double) y);
                }, DispatcherPriority.Normal);
            }
        }

        private void FirstCircle_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var selectedPriority = comboBox.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(selectedPriority))
            {
                if (Enum.TryParse(selectedPriority, out ThreadPriority threadPriority))
                {
                    _threads[0].Priority = threadPriority;
                }
            }
        }

        /// <summary>
        /// Получение целого числа через криптостойкий алгоритм
        /// </summary>
        /// <returns></returns>
        private int GetCryptoRandomNumber()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            var bytes = new byte[4];
            provider.GetBytes(bytes);

            // If the system architecture is little-endian (that is, little end first),
            // reverse the byte array.
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return BitConverter.ToInt32(bytes, 0);
        }

        private void SecondCircle_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var selectedPriority = comboBox.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(selectedPriority))
            {
                if (Enum.TryParse(selectedPriority, out ThreadPriority threadPriority))
                {
                    _threads[1].Priority = threadPriority;
                }
            }
        }

        private void ThirdCircle_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var selectedPriority = comboBox.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(selectedPriority))
            {
                if (Enum.TryParse(selectedPriority, out ThreadPriority threadPriority))
                {
                    _threads[2].Priority = threadPriority;
                }
            }
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            foreach (var thread in _threads)
            {
                thread.Abort();
            }
        }
    }
}