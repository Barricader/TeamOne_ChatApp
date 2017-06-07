using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChatBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBase.Models.Tests
{
    [TestClass()]
    public class PacketFactoryTests
    {
        [TestMethod()]
        public void CreatePacketTest()
        {
            IPacket packet = null;
            PacketType type = PacketType.Message; 
            switch (type)
            {
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

            Assert.IsNotNull(packet);
        }
    }
}