using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBase.Models {
    public class Packet {
        public string Type { get; set; }
        public string Content { get; set; }
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
    }
}
