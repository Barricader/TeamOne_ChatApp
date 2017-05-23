using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace ChatBase.Models {
    [DataContract]
    public class Packet {
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public Dictionary<string, string> Args { get; set; }

        public Packet(string type, string content) {
            Type = type;
            Content = content;
            Args = new Dictionary<string, string>();
        }

        public Packet(string type, string content, Dictionary<string, string> args) {
            Type = type;
            Content = content;
            Args = args;
        }

        public string ToJsonString() {
            DataContractJsonSerializer jSerializer = new DataContractJsonSerializer(typeof(Packet));
            string jsonString = "";

            MemoryStream stream = new MemoryStream();
            StreamReader sr = new StreamReader(stream);

            jSerializer.WriteObject(stream, this);
            stream.Position = 0;

            jsonString = sr.ReadToEnd();

            return jsonString;
        }
    }
}
