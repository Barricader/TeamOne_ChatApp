using ChatBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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


// TODO: use IClient
namespace NewChat {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private TcpClient client = new TcpClient();
        private IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Constants.PORT);
        private Thread listenThread;

        public MainWindow() {
            InitializeComponent();
            client.Connect(serverEP);

            lblBroadcast.Dispatcher.Invoke(() => lblBroadcast.Content = "You have connected to: " + serverEP);

            listenThread = new Thread(new ThreadStart(ReadResponse));
            listenThread.Start();
        }

        private void ReadResponse() {
            NetworkStream stream = client.GetStream();
            byte[] data = new byte[Constants.BUFFER_SIZE];
            Int32 bytes = 0;
            string response = "";

            // TODO: find a better way to listen, like get an event if stream finds input
            while (true) {
                Thread.Sleep(100);

                bytes = stream.Read(data, 0, data.Length);
                response = Encoding.ASCII.GetString(data, 0, bytes);

                if (response.Length > 0) {
                    lblBroadcast.Dispatcher.Invoke(() => lblBroadcast.Content = lblBroadcast.Content + Environment.NewLine + response);

                }
            }
        }

        private void MessageBoxKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter || e.Key == Key.Return) {
                SendMessage(messageBox.Text);

                if (messageBox.Text == "~!bye") {
                    Close();
                }

                messageBox.Dispatcher.Invoke(() => messageBox.Text = "");
            }
        }

        private void SendMessage(string msg) {
            NetworkStream clientStream = client.GetStream();
            
            byte[] buffer = Encoding.UTF8.GetBytes(msg);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();

            // TODO: remove this stuff
            // Get TcpServer.response

            // Buffer to store response
            //byte[] data = new byte[Constants.BUFFER_SIZE];

            //// store ASCII representation
            //string responseData = "";

            //// Read first bytes
            //Int32 bytes = clientStream.Read(data, 0, data.Length);
            //responseData = Encoding.ASCII.GetString(data, 0, bytes);

            ////messageBox.AppendText(Environment.NewLine + "From Server: " + responseData);
            //messageBox.Dispatcher.Invoke(() => messageBox.AppendText(Environment.NewLine + "From Server: " + responseData));
        }

        private void Window_Closed(object sender, EventArgs e) {
            string endMsg = "~!bye";
            SendMessage(endMsg);

            //listenThread.Suspend();
            //listenThread.Join();
            listenThread.Abort();
            client.Close();
        }
    }
}
