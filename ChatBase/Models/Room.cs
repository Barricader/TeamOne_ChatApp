using ChatBase.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChatBase.Models {
    public class Room {
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public List<Message> Messages { get; set; }

        public Room(string name) {
            Name = name;
            Users = new List<User>();
            Messages = new List<Message>();
        }

        public override string ToString() {
            return $"Room name: {Name}, Amount of users in room: {Users.Count()}, Amount of messages: {Messages.Count()}";
        }
    }
}
