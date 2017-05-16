using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBase {
    public static class Constants {
        public static int PORT = 4040;
        public static int BUFFER_SIZE = 4096;
        public static string CLIENT_BYE_MESSAGE = "~!bye";
        public static string SERVER_BYE_MESSAGE = "~!goodbye";

        public delegate void WindowHandler();
    }
}
