using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace ChatBase.Models {
    public enum PacketType {
        Message,
        ClientID,
        Goodbye,
        RequestRoom,
        RequestMessage,
        JoinRoomRequest,
        JoinRoomResponse,
        RequestUser,
        RequestLogin,
        RoomResponse,
        UserJoined,
        RoomCreated,
        Null            // Should never happen
    };

    public class Packet {
        //public string Type { get; set; }
        public PacketType Type { get; set; }
        public string Content { get; set; }
        public Dictionary<string, string> Args { get; set; }

        public Packet () {
            Type = PacketType.Null;
            Content = "";
            Args = new Dictionary<string, string>();
        }

        public Packet(PacketType type, string content) {
            Type = type;
            Content = content;
            Args = new Dictionary<string, string>();
        }

        public Packet(PacketType type, string content, Dictionary<string, string> args) {
            Type = type;
            Content = content;
            Args = args;
        }

        public Packet AlterContent(string content) {
            Content = content;

            return this;
        }

        public string ToJsonString() {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonString = "";

            jsonString = jss.Serialize(this);

            return jsonString;
        }

        /// <summary>
        /// Convert json string to Packet object
        /// </summary>
        /// <param name="json">Json string to convert</param>
        /// <param name="packet">Packet object to use</param>
        /// <returns>If conversion was successful then return true, else return false</returns>
        public static bool JsonToPacket(string json, out Packet packet) {
            // Heal string if first { is not there
            if (json[0] == '"') {
                json = "{" + json;
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            bool result = true;
            Packet p = new Packet();
            packet = null;

            try {
                p = (Packet)jss.Deserialize(json, typeof(Packet));
            } catch (ArgumentException ex) {
                // Incorrect json format
                result = false;
                Console.WriteLine("ERROR: " + ex);
            }

            if (result) {
                packet = p;
            }

            return result;
        }
    }
}
