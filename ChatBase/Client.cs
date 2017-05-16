using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Input;
using static ChatBase.Constants;

namespace ChatBase {
    public class Client : INotifyPropertyChanged {
        private TcpClient client = new TcpClient();
        private IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT);
        private Thread listenThread;

        private string broadcast = "";
        private string windowTitle = "";
        private string curMessage = "";

        public event PropertyChangedEventHandler PropertyChanged;
        public event WindowHandler windowHandler;

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

        public void Start() {
            listenThread = new Thread(new ThreadStart(TryConnect));
            listenThread.Start();
        }

        private void TryConnect() {
            int curTry = 0;
            bool succeeded = false;

            while (curTry < RECONNECT_MAX_TRIES && !succeeded) {
                try {
                    curTry++;
                    Broadcast += "TRY " + curTry + ": Attempting to connect to " + serverEP + Environment.NewLine;
                    client.Connect(serverEP);
                    succeeded = true;
                } catch (SocketException ex) {
                    succeeded = false;
                    Broadcast += "Failed to connect to " + serverEP + "... Trying again in " + SECONDS_BETWEEEN_TRIES + " seconds..."+ Environment.NewLine;
                    Thread.Sleep(SECONDS_BETWEEEN_TRIES * 1000);
                }
            }

            if (curTry == RECONNECT_MAX_TRIES) {
                Broadcast += "It seems that the you or the server is having connection issues, please try again later..." + Environment.NewLine;
                Thread.Sleep(1000);
                Window_Closed(null, null);
                windowHandler?.Invoke();
            }

            Broadcast += "You have connected to: " + serverEP;
            ReadResponse();
        }

        private void ReadResponse() {
            NetworkStream stream = client.GetStream();
            byte[] data = new byte[BUFFER_SIZE];
            Int32 bytes = 0;
            string response = "";

            // TODO: if get error, try reconnecting
            // TODO: find a better way to listen, like get an event if stream finds input
            while (true) {
                Thread.Sleep(100);

                bytes = stream.Read(data, 0, data.Length);
                response = Encoding.UTF8.GetString(data, 0, bytes);

                if (response.Length > 0) {
                    // Use json here to tell if type of message is not cmd
                    if (response == "~!goodbye") {
                        Broadcast += Environment.NewLine + "Server has shutdown, closing connection...";

                        //listenThread.Abort();
                        client.Close();
                        break;
                    }
                    else if (response.Contains("~!client")) {
                        response = response.Replace("~!client", "");
                        WindowTitle = "Connected to " + serverEP.Address + " | Client " + response;
                    }
                    else {
                        Broadcast += Environment.NewLine + response;
                    }
                }
            }
        }

        public void MessageBoxKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter || e.Key == Key.Return) {
                SendMessage(CurMessage);

                if (CurMessage == Constants.CLIENT_BYE_MESSAGE) {
                    // Close the client window if the kill message is sent
                    Window_Closed(null, null);
                    windowHandler?.Invoke();
                }

                CurMessage = "";
            }
        }

        private void SendMessage(string msg) {
            if (client.Connected) {
                NetworkStream clientStream = client.GetStream();

                byte[] buffer = Encoding.UTF8.GetBytes(msg);

                clientStream.Write(buffer, 0, buffer.Length);
                clientStream.Flush();
            }
        }

        public void Window_Closed(object sender, EventArgs e) {
            string endMsg = CLIENT_BYE_MESSAGE;
            SendMessage(endMsg);

            listenThread.Abort();
            client.Close();
        }

        private void PropChanged([CallerMemberName] string prop = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
