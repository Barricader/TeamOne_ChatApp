using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBase.Models
{
    public class Room
    {
        public Room(string roomName)
        {
            Name = roomName;
        }
        private string name;
        public string Name { get { return name; } private set { name = value; } }
        public List<User> users = new List<User>();
        public List<Message> messages = new List<Message>();
        public override string ToString()
        {
            return "Room name: " + Name + "Amount of Users in room: " + users.Count();
        }
    }
}
