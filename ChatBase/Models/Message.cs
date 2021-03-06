﻿using ChatBase.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBase.Models {
    public enum MessageType {
        String,
        Image,
        File,       // max size of file is 4MB
        Link
    }

    // TODO: emoji static class that links strings to images EX. ':smiley:' -> Image smiley

    public class Message {
        public Message(User owner, Room room, string content, DateTime ts) {
            Owner = owner;
            OwningRoom = room;
            Content = content;
            MessageTimeStamp = ts;
            TimeStamp = MessageTimeStamp.ToString("MMMM d") + " at " + MessageTimeStamp.ToString("h:mm tt");
        }
        private User owner;
        public User Owner { get { return owner; } private set { owner = value; } }
        private Room owningRoom;
        public Room OwningRoom { get { return owningRoom; } set { owningRoom = value; } }
        private string content;
        public string Content { get { return content; } set { content = value; } }
        public MessageType Type { get; set; }
        public DateTime MessageTimeStamp { get; set; }
        public string TimeStamp { get; set; }
        public override string ToString() {
            return $"Type: {Type}, Owner: {Owner}, Room: {OwningRoom}, Content: {Content}";
        }
    }

}
