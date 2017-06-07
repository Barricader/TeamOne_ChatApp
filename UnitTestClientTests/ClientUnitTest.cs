using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChatBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Input;
using static ChatBase.Constants;
using ChatBase;
using System.Net;
using System.Threading;

namespace UnitTestClientTests
{
    [TestClass]
    public class ClientUnitTest
    {
        private TcpClient client = new TcpClient();
        private IPEndPoint serverEP;
        private Thread listenThread;

        [TestMethod]
        public void TestMethod1()
        {

        }
        [TestMethod]
        public void StartAccept_ValidIP()
        {
            Client client = new Client();
            client.Start();
            string actual = ip;
            Assert.AreEqual(expected,actual)
        }
       

    }
}
