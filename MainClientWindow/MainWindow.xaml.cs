using ChatBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace MainClientWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            Client client = (Client)FindResource("client");
            client.Start();

            mainframe.NavigationService.Navigate(new Uri("ChatPage.xaml", UriKind.RelativeOrAbsolute));

            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon("../../icons/icon.ico");
            ni.Visible = true;
            ni.Click +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };


            //client.msgReceived += GotMessage;   // event that gets called when server sends a message

            //Client client = new Client();

            //DataContext = client;

            //Closed += client.Window_Closed;


            //client.windowHandler += CloseWindow;

            //client.Start();
        }

        private void GotMessage(string msg) {
            //Console.WriteLine(msg);
        }

        private void newMsg(string msg) {
            Console.WriteLine(msg);
        }

        public void CloseWindow()
        {
            Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
