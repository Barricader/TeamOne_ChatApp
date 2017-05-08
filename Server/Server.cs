using ChatBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server {
    public class Server : IServer {
        
    }

    public class Startup {
        public static void Start() {
            Server server = new Server();


        }

        public static void Main() {
            Start();
        }
    }
}
