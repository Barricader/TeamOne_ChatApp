using ChatBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;

namespace ChatBase {
    public class Server : INotifyPropertyChanged {
        // Variables to allow listening
        private TcpListener tcpListener;
        private Thread listenThread;

        // Variables that keep client state
        private int connClients = 0;
        private List<Thread> clientThreads = new List<Thread>();
        private List<TcpClient> clients = new List<TcpClient>();
        private int nextClientID = 1;

        private bool isClosing = false;
        private string messagesReceived = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public string MessagesReceived {
            get => messagesReceived;
            set {
                messagesReceived = value;
                PropChanged();
            }
        }
        public int ConnClients {
            get => connClients;
            set {
                connClients = value;
                PropChanged();
            }
        }

        /// <summary>
        /// Start the server
        /// </summary>
        public void Start() {
            // Change to IpAddress.Any for internet communication
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), Constants.PORT);
            listenThread = new Thread(new ThreadStart(ListenForClients));
            listenThread.Start();
        }

        /// <summary>
        /// Begin listening for client connections and create new TcpClients if one does connect
        /// </summary>
        private void ListenForClients() {
            tcpListener.Start();

            // Do not stop until the server is closed
            while (true) {
                // Block until client has connected
                TcpClient client = tcpListener.AcceptTcpClient();

                // Create thread to handle communication with client
                ConnClients++;

                Thread clientThread = new Thread(new ParameterizedThreadStart(ClientCommsHandler));
                clientThreads.Add(clientThread);
                clients.Add(client);
                clientThread.Start(client);
            }
        }

        /// <summary>
        /// Listen for messages from the client
        /// </summary>
        /// <param name="client">TcpClient to listen to</param>
        private void ClientCommsHandler(object client) {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();
            int clientID = nextClientID++;

            byte[] msg = new byte[Constants.BUFFER_SIZE];
            int bytesRead;

            // Send client their clientID
            string clMsg = "~!client" + clientID;
            SendMessage(clMsg, clientStream);
            Thread.Sleep(150);  // Need to wait a bit because it will be one big message if we don't
            string welcomeMessage = "You are client " + clientID + Environment.NewLine;
            SendMessage(welcomeMessage, clientStream);
            WriteMessage("Client " + clientID + " has connected!");

            // Listen for client response
            while (true) {
                bytesRead = 0;

                try {
                    // Block until message received
                    bytesRead = clientStream.Read(msg, 0, Constants.BUFFER_SIZE);
                } catch (SocketException sockEx) {
                    if (isClosing) {
                        // Don't do anything because the server is stopping
                        break;
                    }
                    else {
                        MessageBox.Show(sockEx.ToString());
                        break;
                    }
                } catch (Exception ex) {
                    // TODO: get better exceptions
                    //MessageBox.Show(ex.ToString());
                    break;
                }

                // Convert bytes to string and display string
                string message = Encoding.UTF8.GetString(msg, 0, bytesRead);

                // TODO: split up message events to run on their own delegate for easier readability and maintainability
                // Example: when user sends bye message, run the User_Left(clientID) method

                // User sent kill signal
                if (message == Constants.CLIENT_BYE_MESSAGE) {
                    string leaveMsg = "Client " + clientID + " has left...";
                    Broadcast(leaveMsg);
                    WriteMessage(leaveMsg);
                    ConnClients--;
                    clients.Remove(tcpClient);
                    break;
                }

                string newMsg = "Client " + clientID + " says: " + message;
                WriteMessage(newMsg);

                // Send message to all clients
                Broadcast(newMsg);
            }

            tcpClient.Close();
        }

        /// <summary>
        /// Write message to log textbox
        /// </summary>
        /// <param name="msg"></param>
        private void WriteMessage(string msg) {
            MessagesReceived += msg + Environment.NewLine;
        }

        /// <summary>
        /// Send message to specific client
        /// </summary>
        /// <param name="msg">Message to send</param>
        /// <param name="clientStream">Client stream to send to</param>
        private void SendMessage(string msg, NetworkStream clientStream) {
            Packet p = new Packet("Message", msg);

            string newMsg = p.ToJsonString();

            Console.WriteLine(newMsg);

            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
        }

        /// <summary>
        /// Send message to all connected clients
        /// </summary>
        /// <param name="msg"></param>
        private void Broadcast(string msg) {
            // Echo message back
            byte[] buffer = Encoding.UTF8.GetBytes(msg);

            // Echo to all dudes
            foreach (TcpClient client in clients) {
                client.GetStream().Write(buffer, 0, buffer.Length);
                client.GetStream().Flush();
            }
        }

        /// <summary>
        /// When server is closed, dispose all the things
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Window_Closed(object sender, EventArgs e) {
            isClosing = true;

            // Send server bye message to clients so they know that the server is shutting down
            foreach (TcpClient cl in clients) {
                SendMessage(Constants.SERVER_BYE_MESSAGE, cl.GetStream());
                cl.Close();
            }
            
            tcpListener.Stop();
            listenThread.Abort();
        }

        /// <summary>
        /// If we are in the process of closing, let keep it in state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            isClosing = true;
        }

        /// <summary>
        /// If a property is changed, invoke PropertyChanged event
        /// </summary>
        /// <param name="prop"></param>
        private void PropChanged([CallerMemberName] string prop = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
