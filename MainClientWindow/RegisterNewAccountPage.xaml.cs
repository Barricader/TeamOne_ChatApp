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

namespace MainClientWindow
{
    /// <summary>
    /// Interaction logic for RegisterNewAccountPage.xaml
    /// </summary>
    public partial class RegisterNewAccountPage : Page
    {
        public RegisterNewAccountPage()
        {
            InitializeComponent();
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            bool validPass = ConfirmPassword(password.Text, confirmPassword.Text);
            bool validInput = InputValidation();
            if (validPass && !validInput)
            {
                //Code after user submits valid input                
            }
            else
            {
                CreateErrorMessage(validPass, validInput);
            }           
        }

        private void CreateErrorMessage(bool pass, bool input)
        {
            if (!pass)
            {
                errorMsg.Content = "Passwords Do Not Match";
                password.Background = Brushes.Red;
                confirmPassword.Background = Brushes.Red;
            }
            if (input)
            {
                if(!pass) errorMsg.Content += " - Invalid Input";
                if(pass) errorMsg.Content = " Invalid Input ";
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

        private bool InputValidation()
        {
            bool invalidInput = false;
            if (string.IsNullOrEmpty(firstName1.Text.Trim()))
            {
                firstName1.Background = Brushes.Red;
                invalidInput = true;
            }
            if (string.IsNullOrEmpty(lastName.Text.Trim()))
            {
                lastName.Background = Brushes.Red;
                invalidInput = true;
            }
            if (string.IsNullOrEmpty(gender.Text.Trim()))
            {
                gender.Background = Brushes.Red;
                invalidInput = true;
            }
            if (string.IsNullOrEmpty(password.Text.Trim()))
            {
                password.Background = Brushes.Red;
                invalidInput = true;
            }
            if (string.IsNullOrEmpty(confirmPassword.Text.Trim()))
            {
                confirmPassword.Background = Brushes.Red;
                invalidInput = true;
            }
            if (string.IsNullOrEmpty(screenName.Text.Trim()))
            {
                screenName.Background = Brushes.Red;
                invalidInput = true;
            }
            CheckForCorrection();
            return invalidInput;
        }

        private void CheckForCorrection()
        {
            if (!string.IsNullOrEmpty(firstName1.Text.Trim())) firstName1.Background = Brushes.White;
            if (!string.IsNullOrEmpty(lastName.Text.Trim())) lastName.Background = Brushes.White;
            if (!string.IsNullOrEmpty(gender.Text.Trim())) gender.Background = Brushes.White;
            if (!string.IsNullOrEmpty(password.Text.Trim())) password.Background = Brushes.White;
            if (!string.IsNullOrEmpty(confirmPassword.Text.Trim())) confirmPassword.Background = Brushes.White;
            if (!string.IsNullOrEmpty(screenName.Text.Trim())) screenName.Background = Brushes.White;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("LoginPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
