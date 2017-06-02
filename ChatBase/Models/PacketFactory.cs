using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace ChatBase.Models {
    // Based on Keith Ball's example in his ScratchPad project
    public static class PacketFactory {
        // Makes it easy to convert from PackeType to the class representation
        private static Dictionary<PacketType, Type> typeToClass = new Dictionary<PacketType, Type> {
            { PacketType.Message, typeof(MessagePacket) },
            { PacketType.ClientID, typeof(ClientIDPacket) },
            { PacketType.Goodbye, typeof(GoodByePacket) },
            { PacketType.RequestAllRooms, typeof(RequestAllRoomsPacket) },
            { PacketType.RequestCreateRoom, typeof(RequestCreateRoomPacket) },
            { PacketType.RequestUser, typeof(RequestUserPacket) },
            { PacketType.ResponseAllRooms, typeof(ResponseAllRoomsPacket) },
            { PacketType.ResponseAllUsers, typeof(ResponseAllUsersPacket) },
            { PacketType.UserJoined, typeof(UserJoinedPacket) },
            { PacketType.RoomCreated, typeof(RoomCreatedPacket) }
        };

        /// <summary>
        /// Traditional factory
        /// </summary>
        /// <param name="type">Type of packet to create</param>
        /// <returns>Packet type given</returns>
        public static IPacket CreatePacket(PacketType type) {
            IPacket packet = null;

            switch (type) {
                case PacketType.Null:   // Should never happen
                    break;
                case PacketType.Message:
                    packet = new MessagePacket();
                    break;
                case PacketType.ClientID:
                    packet = new ClientIDPacket();
                    break;
                case PacketType.Goodbye:
                    packet = new GoodByePacket();
                    break;
                case PacketType.RequestAllRooms:
                    packet = new RequestAllRoomsPacket();
                    break;
                case PacketType.RequestCreateRoom:
                    packet = new RequestCreateRoomPacket();
                    break;
                case PacketType.RequestUser:
                    packet = new RequestUserPacket();
                    break;
                case PacketType.ResponseAllRooms:
                    packet = new ResponseAllRoomsPacket();
                    break;
                case PacketType.ResponseAllUsers:
                    packet = new ResponseAllUsersPacket();
                    break;
                case PacketType.UserJoined:
                    packet = new UserJoinedPacket();
                    break;
                case PacketType.RoomCreated:
                    packet = new RoomCreatedPacket();
                    break;
                default:            // Should never happen
                    break;
            }

            return packet;
        }

        /// <summary>
        /// Cool generic factory which creates a specified packet based on the generic type given
        /// </summary>
        /// <typeparam name="T">Specific packet type (Ex. MessagePacket)</typeparam>
        /// <returns>Specified packet type</returns>
        public static T CreatePacket<T>() where T : IPacket, new() {
            return new T();
        }

        /// <summary>
        /// Convert json string to Packet object
        /// </summary>
        /// <param name="json">Json string to convert</param>
        /// <param name="packet">IPacket object to use</param>
        /// <returns>If conversion was successful then return true, else return false</returns>
        public static bool JsonToPacket<T>(string json, out T packet) where T : IPacket {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            bool result = true;

            // Heal string if first { is not there
            if (json[0] == '"' || json[0] == ' ') {
                json = "{" + json;
            }

            //Get type
            MessagePacket tempPacket = null;
            try {
                tempPacket = jss.Deserialize<MessagePacket>(json);
            } catch (ArgumentException ex) {
                // Incorrect json format
                result = false;
                Console.WriteLine("TYPE_ERROR: " + ex);
            }

            try {
                Type t = typeToClass[tempPacket.Type];
                packet = (T)jss.Deserialize(json, t);
            } catch (ArgumentException ex) {
                // Incorrect json format
                result = false;
                packet = default(T);

                Console.WriteLine("ERROR: " + ex);
            }

            return result;
        }
    }
}
