using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainClientWindow
{
    class Room
    {
        public Room(string userName)
        {
            Name = userName;
            
        }
        private string name;
        public string Name { get { return name; } private set { name = value; } }
        public List<TempUser> users = new List<TempUser>();
        public List<TempMessages> messages = new List<TempMessages>();
    }
    class TempUser
    {


    }
    class TempMessages
    {

    }
    class TestClass
    {
        Room room = new Room("TestPerson");
        public void testStuff()
        {
            
        }
    }
}
