using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace ChatBase.Models {
    public enum PacketType {
        Message,
        ClientID,
        Null        // SHould never happen
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

        public static Packet JsonToPacket(string json) {
            JavaScriptSerializer jss = new JavaScriptSerializer();

            Packet p = (Packet)jss.Deserialize(json, typeof(Packet));
            
            return p;
        }
    }
}
