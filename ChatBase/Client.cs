using ChatBase.Models;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Input;
using static ChatBase.Constants;

namespace ChatBase {
    public class Client : INotifyPropertyChanged {
        // Variables that allow listening to server
        private TcpClient client = new TcpClient();
        private IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT);
        private Thread listenThread;

        // Variables that change UI
        private string broadcast = "";
        private string windowTitle = "";
        private string curMessage = "";
        private string serverMessage = "";
        private bool isRunning = true;

        public event PropertyChangedEventHandler PropertyChanged;
        public event WindowHandler WindowHandler;
        public event MessageReceived MsgReceived;

        public string Broadcast {
            get => broadcast;
            set {
                broadcast = value;
                PropChanged();
            }
        }
        public string WindowTitle {
            get => windowTitle;
            set {
                windowTitle = value;
                PropChanged();
            }
        }
        public string CurMessage {
            get => curMessage;
            set {
                curMessage = value;
                PropChanged();
            }
        }

        public string ServerMessage {
            get => serverMessage;
            set {
                serverMessage = value;
                //msgRcdEvent?.Invoke(serverMessage);
                //msgReceived(serverMessage);
                MsgReceived?.Invoke(serverMessage);
            }
        }

        /// <summary>
        /// Start the listening thread that initializes the server
        /// </summary>
        public void Start() {
            listenThread = new Thread(new ThreadStart(Init));
            listenThread.Start();
        }

        /// <summary>
        /// Connect to server and start reading from server
        /// </summary>
        private void Init() {
            Reconnect();
            ReadResponse();
        }

        /// <summary>
        /// Try connecting to the server until successful or max tries
        /// </summary>
        private void Reconnect() {
            int curTry = 0;
            bool succeeded = false;

            // TODO: have main client UI show reconnect progress
            // Reconnect until we hit the max amount of tries
            while (curTry < RECONNECT_MAX_TRIES && !succeeded) {
                try {
                    curTry++;
                    Broadcast += "TRY " + curTry + ": Attempting to connect to " + serverEP + Environment.NewLine;
                    client.Connect(serverEP);
                    succeeded = true;
                } catch (SocketException ex) {
                    // This means we failed to connect...
                    succeeded = false;
                    Broadcast += "Failed to connect to " + serverEP + "... Trying again in " + SECONDS_BETWEEEN_TRIES + " seconds..." + Environment.NewLine;
                    Thread.Sleep(SECONDS_BETWEEEN_TRIES * 1000);
                }
            }

            if (curTry == RECONNECT_MAX_TRIES) {
                Broadcast += "It seems that the you or the server is having connection issues, please try again later..." + Environment.NewLine;
                Thread.Sleep(1000);
                Window_Closed(null, null);
                WindowHandler?.Invoke();
            }

            Broadcast += "You have connected to: " + serverEP + Environment.NewLine;
        }

        /// <summary>
        /// Reads any response we get from the server
        /// </summary>
        private void ReadResponse() {
            NetworkStream stream = client.GetStream();
            byte[] data = new byte[BUFFER_SIZE];
            Int32 bytes = 0;
            string response = "";
            
            // TODO: find a better way to listen, like get an event if stream finds input
            while (isRunning) {
                Thread.Sleep(100);  // Sleep for a bit, save some cpu cycles

                try {
                    bytes = stream.Read(data, 0, data.Length);  // Read stream
                } catch (IOException ex) {
                    // Lost connection...
                    client.Close();
                    client = new TcpClient();
                    Reconnect();
                    stream = client.GetStream();
                }

                response = Encoding.UTF8.GetString(data, 0, bytes); // Change bytes to readable string

                // If a message was recieved then do stuff
                if (response.Length > 0) {
                    // If the response is a valid packet, read it
                    if (Packet.JsonToPacket(response, out Packet tempPacket)) {
                        ReadPacket(tempPacket);
                    }
                }
            }
        }

        /// <summary>
        /// Check if enter is pressed, if so then send the message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MessageBoxKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter || e.Key == Key.Return) {
                //SendMessage(CurMessage);
                //SendPacket(MESSAGE_TEMPLATE.AlterContent(CurMessage));

                // TODO: probably don't want client to be able to kill connection with a message??
                // TODO: change packet to GOODBYE???
                //if (CurMessage == CLIENT_BYE_MESSAGE) {
                //    // Close the client window if the kill message is sent
                //    Window_Closed(null, null);
                //    windowHandler?.Invoke();
                //}

                SendPacket(MESSAGE_TEMPLATE.AlterContent(CurMessage));
                CurMessage = "";
            }
        }

        /// <summary>
        /// Read a received Packet and perform action based on type
        /// </summary>
        /// <param name="json">Json string of Packet</param>
        private void ReadPacket(Packet p) {
            switch (p.Type) {
                case PacketType.ClientID:
                    WindowTitle = "Connected to " + serverEP.Address + " | Client " + p.Content;
                    break;
                case PacketType.Message:
                    Broadcast += Environment.NewLine + p.Content;
                    ServerMessage = p.Content;
                    break;
                case PacketType.Goodbye:
                    Broadcast += "Server has shutdown, closing connection..." + Environment.NewLine;
                    Console.WriteLine("RIP SERVER");
                    client.Close();
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("ERROR: wrong packet type........... Content: {0]", p.Content);
                    break;
            }
        }

        private void SendPacket(Packet p) {
            if (client.Connected) {
                NetworkStream clientStream = client.GetStream();
                string json = p.ToJsonString();

                byte[] buffer = Encoding.UTF8.GetBytes(json);

                clientStream.Write(buffer, 0, buffer.Length);
                clientStream.Flush();
            }
        }

        /// <summary>
        /// If the client window is closed, dispose of everything
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Window_Closed(object sender, EventArgs e) {
            SendPacket(CLIENT_BYE_PACKET);

            listenThread.Abort();
            client.Close();
        }

        /// <summary>
        /// If a property is changed, invoke the PropertyChanged event
        /// </summary>
        /// <param name="prop"></param>
        private void PropChanged([CallerMemberName] string prop = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
