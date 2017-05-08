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
using System.Windows.Threading;

/*
 * Notes:
 *  Using port 4040/tcp to connect to server and transmit messages
 * 
 * 
 * 
 *
 */

namespace Server {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public SocketPermission Permission { get; set; }
        public Socket SockListener { get; set; }
        public IPEndPoint EndPoint { get; set; }
        public Socket SockHandler { get; set; }

        private TextBox auxilary = new TextBox();

        public MainWindow() {
            InitializeComponent();
            auxilary.SelectionChanged += AuxSelChanged;
        }

        private void AuxSelChanged(object sender, RoutedEventArgs e) {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate () {
                msgReceived.Text = auxilary.Text;
            });
        }

        private void StartButton_Click(object sender, RoutedEventArgs e) {
            try {
                // Setup a SocketPermission obj for access control
                Permission = new SocketPermission(
                                    NetworkAccess.Accept,       // Can accept connections
                                    TransportType.Tcp,          // Use tcp for connections
                                    "",                         // IP of local host
                                    SocketPermission.AllPorts); // Can use all ports

                // Set listener to null for now
                SockListener = null;

                // Makes sure that we have permission to access the socket
                Permission.Demand();

                // Resolve the hostname to an IPHostEntry instance
                IPHostEntry ipHost = Dns.GetHostEntry("");

                // Get IP associated with localhost
                IPAddress ipAddr = ipHost.AddressList[0];

                // Create endpoint
                EndPoint = new IPEndPoint(ipAddr, 4040);

                // Create a Socket obj to listen for connections
                SockListener = new Socket(
                                    ipAddr.AddressFamily,
                                    SocketType.Stream,
                                    ProtocolType.Tcp);

                // Bind our endpoint to the socket
                SockListener.Bind(EndPoint);

                labelStatus.Content = "Server started...";

                startButton.IsEnabled = false;
                listenButton.IsEnabled = true;
            }
            catch {

            }
        }

        private void ListenButton_Click(object sender, RoutedEventArgs e) {

        }

        private void SendButton_Click(object sender, RoutedEventArgs e) {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) {

        }
    }
}
