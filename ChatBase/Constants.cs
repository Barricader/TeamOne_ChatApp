using ChatBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBase {
    public static class Constants {
        public static int PORT = 4040;
        public static int BUFFER_SIZE = 4096;
        public static int RECONNECT_MAX_TRIES = 10;
        public static int SECONDS_BETWEEEN_TRIES = 5;

        public static Dictionary<string, string> SERVER_AUTHENTICATED = new Dictionary<string, string>() { { "Authentic_Server", "YES" } };
        public static Dictionary<string, string> CLIENT_AUTHENTICATED = new Dictionary<string, string>() { { "Authentic_Client", "YES" } };

        public static Packet SERVER_BYE_PACKET = new Packet("Message", "Goodbye", SERVER_AUTHENTICATED);
        public static Packet CLIENT_BYE_PACKET = new Packet("Message", "Goodbye", CLIENT_AUTHENTICATED);

        public static string CLIENT_BYE_MESSAGE = "~!bye";
        public static string SERVER_BYE_MESSAGE = "~!goodbye";

        public delegate void WindowHandler();
        public delegate void MessageReceived(string msg);
    }
}
