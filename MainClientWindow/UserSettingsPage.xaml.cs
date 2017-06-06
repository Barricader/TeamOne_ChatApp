using ChatBase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainClientWindow
{
    /// <summary>
    /// Interaction logic for UserSettingsPage.xaml
    /// </summary>
    public partial class UserSettingsPage : Page
    {       
        //static public BitmapImage Img { get; set; }
        public UserSettingsPage()
        {
            InitializeComponent();
        }

        private void CancelToggleButton_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new Uri("ChatPage.xaml", UriKind.RelativeOrAbsolute));
            this.NavigationService.Navigate(new Uri("ChatPage.xaml", UriKind.RelativeOrAbsolute));
            //Console.WriteLine("XD");
            // Do something here to load state ?
        }

        public void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog() {
                Title = "Select a picture",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png"
            };
            if (op.ShowDialog() == true)
            {
                //set user avatar here
                //Img = new BitmapImage(new Uri(op.FileName));
                userAvatar.Source = new BitmapImage(new Uri(op.FileName));    
                Chat.Img = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void NewUsernameToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ResetErrors();
            if (string.IsNullOrEmpty(NewUsername.Text.Trim()))
            {
                NewUsername.Background = Brushes.Red;
                ErrorMessage.Content = "Invalid Username";
            }
            else
            {
                //Set new username here 
                this.NavigationService.Navigate(new Uri("ChatPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void NewPasswordToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ResetErrors();
            if (PasswordMatch(NewPassword.Text, ConfirmPassword.Text))
            {
                if (string.IsNullOrEmpty(NewPassword.Text.Trim()) || string.IsNullOrEmpty(ConfirmPassword.Text.Trim()))
                {
                    ErrorMessage.Content = "Password Is Empty Or Null";                   
                }
                else
                {
                    //set new password here
                    this.NavigationService.Navigate(new Uri("ChatPage.xaml", UriKind.RelativeOrAbsolute));
                }
            }
            else
            {
                ErrorMessage.Content = "Passwords Do Not Match";
            }           
            NewPassword.Background = Brushes.Red;
            ConfirmPassword.Background = Brushes.Red;
        }

        private bool PasswordMatch(string pass1, string pass2)
        {
            bool valid = false;
            if (pass1.Equals(pass2)) valid = true;
            return valid;
        }

        private void ResetErrors()
        {
            ErrorMessage.Content = "";
            NewPassword.Background = Brushes.GhostWhite;
            ConfirmPassword.Background = Brushes.GhostWhite;
            NewUsername.Background = Brushes.GhostWhite;           
        }
    }
}
