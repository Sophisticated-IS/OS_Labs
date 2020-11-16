using System;
using System.Windows;
using LogicLib;

namespace Task5_Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IPC _serverIPC;

        public MainWindow()
        {
            InitializeComponent();
            _serverIPC = new IPC();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _serverIPC.SendMessageToClient("Method1");
        }

        private void ButtonBase_OnClick2(object sender, RoutedEventArgs e)
        {
            _serverIPC.SendMessageToClient("Method2");
        }

        private void ButtonBase_OnClick3(object sender, RoutedEventArgs e)
        {
            _serverIPC.SendMessageToClient("Method3");
        }

        private void ButtonBase_OnClick4(object sender, RoutedEventArgs e)
        {
            _serverIPC.SendMessageToClient("Method4");
        }

        private void ButtonBase_OnClick5(object sender, RoutedEventArgs e)
        {
            _serverIPC.SendMessageToClient("Method5");

        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            _serverIPC.CloseServer();
        }
    }
}