using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChatBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBase.Models;

namespace ChatBase.Tests
{
    [TestClass()]
    public class ClientTests
    {
        [TestMethod()]
        public void MessageBoxKeyDownTest()
        {
            //making sure packets being sent have things in them.
            MessagePacket p = PacketFactory.CreatePacket<MessagePacket>();
            string messageContent = "This is a sample message";
            p.Owner = "Client1";
            p.Content = messageContent;
            Assert.IsNotNull(p.Content);
        }

        [TestMethod()]
        public void RoomGenerationButtonClickTest()
        {
            string curRoom = "Roomname";
            curRoom = curRoom.Trim();
            RequestCreateRoomPacket p = PacketFactory.CreatePacket<RequestCreateRoomPacket>() as RequestCreateRoomPacket;
            p.Content = curRoom;
            Assert.IsNotNull(p.Content);
        }
    }
}