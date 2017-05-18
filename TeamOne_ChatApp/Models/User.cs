using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TeamOne_ChatApp.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string ScreenName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public BitmapImage Avatar { get; set; }
        //public User(string firstName, string lastName, int age, string gender, string screenName, string password)
        //{
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Age = age;
        //    Gender = gender;
        //    ScreenName = screenName;
        //    Password = password;
        //}
        public void CreateMessage()
        {

        }
        public void JoinRoom()
        {

        }
        public override string ToString()
        {
            return "Name: " + FirstName + " " + LastName + "\nAge: " + Age + "\nGender: " + Gender + "\nScreenName: " + ScreenName + "\nPassword: " + Password + "\nConfirm Pass:" + ConfirmPassword;
        }
    }
}
