using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChatBase.Models {
    public class Room : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public List<Message> Messages { get; set; }
        private int newMessages;

        public int NewMessages {
            get { return newMessages; }
            set {
                newMessages = value;
                PropChanged();
            }
        }


        public Room(string name) {
            Name = name;
            Users = new List<User>();
            Messages = new List<Message>();
            NewMessages = 0;
        }

        private void PropChanged([CallerMemberName] string prop = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override string ToString() {
            return Name;
        }
    }
}
