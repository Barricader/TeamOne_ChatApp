using ChatBase.Models;
using System.Collections.Generic;

namespace ChatBase {
    public static class Constants {
        public static int PORT = 4040;
        public static int BUFFER_SIZE = 4096;
        public static int RECONNECT_MAX_TRIES = 10;
        public static int SECONDS_BETWEEEN_TRIES = 5;
        public static int MAX_MESSAGE_SIZE = 180;

        public delegate void WindowHandler();
        public delegate void MessageReceived(Message msg);
        public delegate void RoomAdded(Room room);
        public delegate void HasRoom();
    }
}
