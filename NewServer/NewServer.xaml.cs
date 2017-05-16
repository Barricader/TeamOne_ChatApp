using System;
using System.Collections.Generic;
using System.Linq;
using ChatBase;
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
