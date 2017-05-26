using ChatBase.Models;
using System.Collections.Generic;

namespace ChatBase {
    public static class Constants {
        public static int PORT = 4040;
        public static int BUFFER_SIZE = 4096;
        public static int RECONNECT_MAX_TRIES = 10;
        public static int SECONDS_BETWEEEN_TRIES = 5;

        public static Dictionary<string, string> MESSAGE_ARGS = new Dictionary<string, string> { { "Room", "" }, { "Owner", "" } };

        public static Packet SERVER_BYE_PACKET = new Packet(PacketType.Goodbye, "Server");
        public static Packet CLIENT_BYE_PACKET = new Packet(PacketType.Goodbye, "Client");
        public static Packet MESSAGE_PACKET = new Packet(PacketType.Message, "", MESSAGE_ARGS);
        public static Packet CLIENT_ID_PACKET = new Packet(PacketType.ClientID, "");
        public static Packet REQUEST_ROOM_PACKET = new Packet(PacketType.RequestRoom, "");
        /// <summary>
        /// Should have comma-delimited message ids
        /// </summary>
        public static Packet REQUEST_MESSAGE_PACKET = new Packet(PacketType.RequestMessage, "");
        public static Packet REQUEST_USER_PACKET = new Packet(PacketType.RequestUser, "");
        public static Packet REQUEST_LOGIN_PACKET = new Packet(PacketType.RequestLogin, "");
        public static Packet ROOM_RESPONSE_PACKET = new Packet(PacketType.RoomResponse, "");
        public static Packet USER_JOINED_PACKET = new Packet(PacketType.UserJoined, "");
        public static Packet ROOM_CREATED_PACKET = new Packet(PacketType.RoomCreated, "");

        public delegate void WindowHandler();
        public delegate void MessageReceived(string msg);
    }
}
