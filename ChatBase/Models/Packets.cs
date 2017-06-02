using System.Web.Script.Serialization;

namespace ChatBase.Models {
    public interface IPacket {
        string Content { get; }
        PacketType Type { get; }

        string ToJsonString();
        Packet AlterContent(string content);
    }

    public abstract class Packet : IPacket {
        public string Content { get; set; }
        public PacketType Type { get; set; }

        // Abstract the above

        public string ToJsonString() {
            return new JavaScriptSerializer().Serialize(this);
        }

        public Packet AlterContent(string content) {
            Content = content;

            return this;
        }
    }

    public class MessagePacket : Packet {
        public string Owner { get; set; }
        public string Room { get; set; }
        public string Timestamp { get; set; }

        public MessagePacket() {
            Type = PacketType.Message;
        }

        public new MessagePacket AlterContent(string content) {
            Content = content;

            return this;
        }
    }

    public class ClientIDPacket : Packet {
        public ClientIDPacket() {
            Type = PacketType.ClientID;
        }
    }

    public class GoodByePacket : Packet {
        public GoodByePacket() {
            Type = PacketType.Goodbye;
        }
    }

    public class RequestAllRoomsPacket : Packet {
        public RequestAllRoomsPacket() {
            Type = PacketType.RequestAllRooms;
        }
    }

    public class RequestCreateRoomPacket : Packet {
        public RequestCreateRoomPacket() {
            Type = PacketType.RequestCreateRoom;
        }
    }

    public class RequestUserPacket : Packet {
        public RequestUserPacket() {
            Type = PacketType.RequestUser;
        }
    }

    public class ResponseAllRoomsPacket : Packet {
        public ResponseAllRoomsPacket() {
            Type = PacketType.ResponseAllRooms;
        }
    }
    public class ResponseAllUsersPacket : Packet {
        public ResponseAllUsersPacket() {
            Type = PacketType.ResponseAllUsers;
        }
    }
    public class UserJoinedPacket : Packet {
        public UserJoinedPacket() {
            Type = PacketType.UserJoined;
        }
    }
    public class RoomCreatedPacket : Packet {
        public RoomCreatedPacket() {
            Type = PacketType.RoomCreated;
        }
    }
}
