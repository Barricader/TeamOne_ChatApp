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
using TeamOne_ChatApp.Models;

namespace RegisterNewAccountWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();           
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            bool validPass = ConfirmPassword(password.Text, confirmPassword.Text);
            if (validPass)
            {
                User us = new User()
                {
                    FirstName = firstName1.Text,
                    LastName = lastName.Text,
                    Age = int.Parse(age.Text),
                    Gender = gender.Text,
                    ScreenName = screenName.Text,
                    Password = password.Text,
                    ConfirmPassword = confirmPassword.Text
                };
                MessageBox.Show(us.ToString());
            }
            else
            {
                errorMsg.Content = "Invalid Input";
            }                                  
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                userAvatar.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private bool ConfirmPassword(string pass1, string pass2)
        {
            bool valid = false;
            if (pass1.Equals(pass2)) valid = true;
            return valid;
        }

        private void InputValidation()
        {
            string test = firstName1.Text.Trim();
            if (!string.IsNullOrEmpty(test))
            {

            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
