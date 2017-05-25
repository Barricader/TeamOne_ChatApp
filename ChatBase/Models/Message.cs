using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBase.Models
{
    public class Message
    {
        public Message(User owner, Room room, string content)
        {
            Owner = owner;
            OwningRoom = room;
            Content = content;

        }
        private User owner;
        public User Owner { get { return owner;} private set { owner = value;} }
        private Room owningRoom;
        public Room OwningRoom { get { return owningRoom;} set { owningRoom = value;} }
        private string content;
        public string Content { get { return content;} set { content = value;} }
        public override string ToString()
        {
            return "Owner: " + Owner + "Content: " + Content + "Owning room: " + OwningRoom;
        }

    }
    
}
