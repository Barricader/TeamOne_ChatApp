using ChatBase.Models;
using System.Collections.Generic;

namespace ChatBase {
    public static class Constants {
        public static int PORT = 4040;
        public static int BUFFER_SIZE = 4096;
        public static int RECONNECT_MAX_TRIES = 10;
        public static int SECONDS_BETWEEEN_TRIES = 5;

        //public static Dictionary<string, string> SERVER_AUTHENTICATED = new Dictionary<string, string>() { { "Authentic_Server", "YES" } };
        //public static Dictionary<string, string> CLIENT_AUTHENTICATED = new Dictionary<string, string>() { { "Authentic_Client", "YES" } };
        public static Dictionary<string, string> MESSAGE_ARGS = new Dictionary<string, string> { { "Room", "" }, { "Owner", "" } };

        // TODO: set Message to Goodbye
        //public static Packet SERVER_BYE_PACKET = new Packet(PacketType.Goodbye, "Server", SERVER_AUTHENTICATED);
        //public static Packet CLIENT_BYE_PACKET = new Packet(PacketType.Goodbye, "Client", CLIENT_AUTHENTICATED);
        public static Packet SERVER_BYE_PACKET = new Packet(PacketType.Goodbye, "Server");
        public static Packet CLIENT_BYE_PACKET = new Packet(PacketType.Goodbye, "Client");

        public static Packet MESSAGE_TEMPLATE = new Packet(PacketType.Message, "", MESSAGE_ARGS);
        public static Packet CLIENT_ID_TEMPLATE = new Packet(PacketType.ClientID, "");

        //public static string CLIENT_BYE_MESSAGE = "~!bye";
        //public static string SERVER_BYE_MESSAGE = "~!goodbye";

        public delegate void WindowHandler();
        public delegate void MessageReceived(string msg);
    }
}
