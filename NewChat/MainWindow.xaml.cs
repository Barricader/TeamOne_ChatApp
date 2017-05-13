using ChatBase;
using System;
using System.Collections.Generic;
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


// TODO: use IClient
namespace NewChat {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private TcpClient client = new TcpClient();
        private IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Constants.PORT);

        public MainWindow() {
            InitializeComponent();
            client.Connect(serverEP);
        }

        private void MessageBoxKeyDown(object sender, KeyEventArgs e) {
            if (e.Key != Key.Enter || e.Key != Key.Return) {

            }
            else {
                SendMessage(messageBox.Text);
                messageBox.Dispatcher.Invoke(() => messageBox.Text = "");
            }
        }

        private void SendMessage(string msg) {
            NetworkStream clientStream = client.GetStream();
            
            byte[] buffer = Encoding.UTF8.GetBytes(msg);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();

            // Get TcpServer.response

            // Buffer to store response
            byte[] data = new byte[Constants.BUFFER_SIZE];

            // store ASCII representation
            string responseData = "";

            // Read first bytes
            Int32 bytes = clientStream.Read(data, 0, data.Length);
            responseData = Encoding.ASCII.GetString(data, 0, bytes);

            //messageBox.AppendText(Environment.NewLine + "From Server: " + responseData);
            //messageBox.Dispatcher.Invoke(() => messageBox.AppendText(Environment.NewLine + "From Server: " + responseData));
        }
    }
}
