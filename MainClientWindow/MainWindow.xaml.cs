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

            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon() {
                Icon = new System.Drawing.Icon("../../icons/icon.ico"),
                Visible = true
            };

            ni.Click +=
                delegate (object sender, EventArgs args) {
                    Show();
                    WindowState = WindowState.Normal;
                };

            Closed += client.Window_Closed;
            client.WindowHandler += CloseWindow;
        }

        public void CloseWindow() {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            e.Cancel = true;
            this.Hide();
        }
    }
}
