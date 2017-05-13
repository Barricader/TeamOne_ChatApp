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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            int roomNumbers = 3;

            for (int i = 0; i < roomNumbers; i++)
            {
                System.Windows.Controls.Button newBtn = new Button();

                newBtn.Content = "ROOMNAME " + i.ToString();
                newBtn.Name = "Button" + i.ToString();

                LeftStackTop.Children.Add(newBtn);
            }

            for (int i = 0; i < roomNumbers; i++)
            {
                System.Windows.Controls.Button newBtn = new Button();

                newBtn.Content = "USERNAME" + i.ToString();
                newBtn.Name = "Button" + i.ToString();

                LeftStackBottom.Children.Add(newBtn);
            }
        }

    }
}
