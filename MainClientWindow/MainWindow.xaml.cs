using ChatBase;
using System;
using System.Windows;
using ChatBase.Models;

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

            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon() {
                Icon = Properties.Resources.iconthing,
                Visible = true
            };

            ni.Click += delegate (object sender, EventArgs args) {
                Show();
                WindowState = WindowState.Normal;
            };
            
            Closed += client.Window_Closed;
            client.WindowHandler += CloseWindow;
            

        }

        public void CloseWindow() {
            Close();
        }

        private void Window_StateChanged(object sender, EventArgs e) {
            Hide();
        }
        
    }
}
