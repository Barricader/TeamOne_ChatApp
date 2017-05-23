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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        public void LoginButtonHandler_Click(Object sender, EventArgs e)
        {
            SendData();
        }
        public void SendData()
        {
            this.NavigationService.Navigate(new Uri("ChatPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RegisterAccountButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("RegisterNewAccountPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void PasswordTextChanged(object sender, RoutedEventArgs e)
        {
            
            PassHint.Visibility = Visibility.Collapsed;
        }

        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            UserNameTextBlock.Visibility = Visibility.Collapsed;
        }
    }
}
