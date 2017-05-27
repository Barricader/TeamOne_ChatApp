using ChatBase;
using System;
using System.Windows;

namespace MainClientWindow {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();

            Client client = (Client)FindResource("client");
            client.Start();

            mainframe.NavigationService.Navigate(new Uri("ChatPage.xaml", UriKind.RelativeOrAbsolute));

            Closed += client.Window_Closed;
            client.WindowHandler += CloseWindow;
        }

        public void CloseWindow() {
            Close();
        }
    }
}
