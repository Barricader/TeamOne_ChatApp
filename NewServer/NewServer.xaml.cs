using ChatBase;
using System.Windows;

// TODO: only 1 server allowed, don't let another one start if one is already listening

namespace NewServer {
    public partial class ServerWindow : Window {
        public ServerWindow() {
            InitializeComponent();
            Server server = new Server();

            DataContext = server;

            Closed += server.Window_Closed;
            Closing += server.Window_Closing;

            server.Start();
        }
    }
}
