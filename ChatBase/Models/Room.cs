using ChatBase.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ChatBase.Models {
    public class Room : INotifyPropertyChanged{
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public List<Message> Messages { get; set; }
        private int newMessages;

        public int NewMessages
        {
            get { return newMessages; }
            set
            {
                newMessages = value;
                PropChanged();
            }
        }


        public Room(string name) {
            Name = name;
            Users = new List<User>();
            Messages = new List<Message>();
        }

        private void PropChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override string ToString() {
            return Name;
            //return $"Room name: {Name}, Amount of users in room: {Users.Count()}, Amount of messages: {Messages.Count()}";
        }
    }
}
