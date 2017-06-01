using ChatBase.Models;
using System.Collections.Generic;

namespace ChatBase {
    public static class Constants {
        public static int PORT = 4040;
        public static int BUFFER_SIZE = 4096;
        public static int RECONNECT_MAX_TRIES = 10;
        public static int SECONDS_BETWEEEN_TRIES = 5;

        public  Dictionary<string, string> MESSAGE_ARGS = new Dictionary<string, string> { { "Room", "" }, { "Owner", "" } , { "Timestamp", "" } };

        public  Packet SERVER_BYE_PACKET = new Packet(PacketType.Goodbye, "Server");
        public  Packet CLIENT_BYE_PACKET = new Packet(PacketType.Goodbye, "Client");
        public  Packet MESSAGE_PACKET = new Packet(PacketType.Message, "", MESSAGE_ARGS);
        public  Packet CLIENT_ID_PACKET = new Packet(PacketType.ClientID, "");
        public  Packet REQUEST_ALL_ROOMS_PACKET = new Packet(PacketType.RequestAllRooms, "");
        public  Packet REQUEST_CREATE_ROOM_PACKET = new Packet(PacketType.RequestCreateRoom, "");
        public  Packet REQUEST_USER_PACKET = new Packet(PacketType.RequestUser, "");
        public  Packet REQUEST_LOGIN_PACKET = new Packet(PacketType.RequestLogin, "");
        public  Packet RESPONSE_ALL_ROOMS_PACKET = new Packet(PacketType.ResponseAllRooms, "");
        public  Packet RESPONSE_ALL_USERS_PACKET = new Packet(PacketType.ResponseAllUsers, "");
        public  Packet USER_JOINED_PACKET = new Packet(PacketType.UserJoined, "");
        public  Packet ROOM_CREATED_PACKET = new Packet(PacketType.RoomCreated, "");

        public delegate void WindowHandler();
        public delegate void MessageReceived(Message msg);
        public delegate void RoomAdded(Room room);
        public delegate void HasRoom();
    }
}
