using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

// TODO: add more comments
namespace ChatBase {
    public class Server : INotifyPropertyChanged {
        private TcpListener tcpListener;
        private Thread listenThread;
        private int connClients = 0;
        private List<Thread> clientThreads = new List<Thread>();
        private List<TcpClient> clients = new List<TcpClient>();
        private bool isClosing = false;
        private int nextClientID = 1;
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

        public void Start() {
            // Change to IpAddress.Any for internet communication
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), Constants.PORT);
            listenThread = new Thread(new ThreadStart(ListenForClients));
            listenThread.Start();
        }
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
                clientThread.Start(client);
            }
        }

        private void ClientCommsHandler(object client) {
            TcpClient tcpClient = (TcpClient)client;
            clients.Add(tcpClient);
            NetworkStream clientStream = tcpClient.GetStream();
            //int clientID = (clients.IndexOf(tcpClient) + 1);
            int clientID = nextClientID++;

            byte[] msg = new byte[Constants.BUFFER_SIZE];
            int bytesRead;

            // Send client their clientID
            string clMsg = "~!client" + clientID;
            SendMessage(clMsg, clientStream);
            Thread.Sleep(150);
            string welcomeMessage = "You are client " + clientID;
            SendMessage(welcomeMessage, clientStream);

            while (true) {
                bytesRead = 0;

                try {
                    // Block until message received
                    bytesRead = clientStream.Read(msg, 0, Constants.BUFFER_SIZE);
                    //bytesRead = clientStream.ReadAsync(msg, 0, Constants.BUFFER_SIZE).Result;
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
                
                if (bytesRead == 0) {
                    // Client disconnected
                    ConnClients--;
                    break;
                }

                // Message received
                UTF8Encoding encoder = new UTF8Encoding();

                // Convert bytes to string and display string
                string message = encoder.GetString(msg, 0, bytesRead);

                // TODO: split up message events to run on their own delegate for easier readability and maintainability
                // Example: when user sends bye message, run the User_Left(clientID) method

                // User sent kill signal
                if (message == Constants.CLIENT_BYE_MESSAGE) {
                    string leaveMsg = "Client " + clientID + " has left...";
                    Echo(leaveMsg, encoder, clientStream);
                    ConnClients--;
                    clients.Remove(tcpClient);
                    break;
                }

                string newMsg = "Client " + clientID + " says: " + message;
                WriteMessage(newMsg);

                // Echo message
                Echo(newMsg, encoder, clientStream);
            }

            tcpClient.Close();
        }

        private void WriteMessage(string msg) {
            MessagesReceived += msg + Environment.NewLine;
        }

        private void SendMessage(string msg, NetworkStream clientStream) {
            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
        }

        private void Echo(string msg, UTF8Encoding encoder, NetworkStream clientStream) {
            // Echo message back
            byte[] buffer = encoder.GetBytes(msg);

            // Echo to all dudes
            foreach (TcpClient client in clients) {
                client.GetStream().Write(buffer, 0, buffer.Length);
                client.GetStream().Flush();
            }
        }

        public void Window_Closed(object sender, EventArgs e) {
            // Send server bye message to clients so they know that the server is shutting down

            //foreach (Thread t in clientThreads) {
            //    t.Abort();
            //}

            isClosing = true;

            foreach (TcpClient cl in clients) {
                SendMessage(Constants.SERVER_BYE_MESSAGE, cl.GetStream());
                cl.Close();
            }

            //listenThread.Join();
            //listenThread.Interrupt();
            tcpListener.Stop();
            listenThread.Abort();
        }

        public void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            isClosing = true;
        }

        private void PropChanged([CallerMemberName] string prop = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
