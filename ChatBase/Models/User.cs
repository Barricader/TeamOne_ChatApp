using System.Windows.Media.Imaging;

namespace ChatBase.Models {
    public class User {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string ScreenName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }     // ?????????
        public BitmapImage Avatar { get; set; }
        public Room CurRoom { get; set; }
        public string Role { get; set; }

        public User() {

        }

        public User(string screenName) {
            ScreenName = screenName;
        }

        public override string ToString() {
            //I'm not sure how to do this? I need to bind it to chatpage.xaml and it calls this methoad.  All I want is the First + Last name
            //Let me know 
            return FirstName + " " + LastName;
        }
    }
}
