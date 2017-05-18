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
    /// Interaction logic for Login_Window.xaml
    /// </summary>
    public partial class Login_Window : Page
    {
        public Login_Window()
        {
            InitializeComponent();
        }
        public void LoginButtonHandler_Click(Object sender, EventArgs e)
        {
            SendData();
        }
        public void SendData()
        {

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}