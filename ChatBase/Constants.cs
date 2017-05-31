using ChatBase.Models;
using System.Collections.Generic;

namespace ChatBase {
    public static class Constants {
        public static int PORT = 4040;
        public static int BUFFER_SIZE = 4096;
        public static int RECONNECT_MAX_TRIES = 10;
        public static int SECONDS_BETWEEEN_TRIES = 5;

        public static Dictionary<string, string> MESSAGE_ARGS = new Dictionary<string, string> { { "Room", "" }, { "Owner", "" } , { "Timestamp", "" } };

        public static Packet SERVER_BYE_PACKET = new Packet(PacketType.Goodbye, "Server");
        public static Packet CLIENT_BYE_PACKET = new Packet(PacketType.Goodbye, "Client");
        public static Packet MESSAGE_PACKET = new Packet(PacketType.Message, "", MESSAGE_ARGS);
        public static Packet CLIENT_ID_PACKET = new Packet(PacketType.ClientID, "");
        public static Packet REQUEST_ALL_ROOMS_PACKET = new Packet(PacketType.RequestAllRooms, "");
        public static Packet REQUEST_CREATE_ROOM_PACKET = new Packet(PacketType.RequestCreateRoom, "");
        public static Packet REQUEST_USER_PACKET = new Packet(PacketType.RequestUser, "");
        public static Packet REQUEST_LOGIN_PACKET = new Packet(PacketType.RequestLogin, "");
        public static Packet RESPONSE_ALL_ROOMS_PACKET = new Packet(PacketType.ResponseAllRooms, "");
        public static Packet RESPONSE_ALL_USERS_PACKET = new Packet(PacketType.ResponseAllUsers, "");
        public static Packet USER_JOINED_PACKET = new Packet(PacketType.UserJoined, "");
        public static Packet ROOM_CREATED_PACKET = new Packet(PacketType.RoomCreated, "");

        public delegate void WindowHandler();
        public delegate void MessageReceived(Message msg);
        public delegate void RoomAdded(Room room);
        public delegate void HasRoom();
    }
}
