using ChatBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
        private TcpClient client = new TcpClient();
        private IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Constants.PORT);

        public MainWindow()
        {
            InitializeComponent();
            client.Connect(serverEP);

            int roomNumbers = 3;

            AddRooms(roomNumbers);
            AddUsers(roomNumbers);

            AddMessages();


        }

        private void AddMessages()
        {
            //get 25 messages from server

            //
            //MessagesStackPanel.Add
            System.Windows.Controls.Grid newGrid = new Grid();
            MessagesStackPanel.Children.Add(newGrid);

            //foreach message in messagesfrom server(25 loops)
            for (int i=0; i<25; i++)
            {
                newGrid.RowDefinitions.Add(new RowDefinition());
                //nested grid 
                System.Windows.Controls.Grid newSubGrid = new Grid();
                newGrid.Children.Add(newSubGrid);
                newSubGrid.RowDefinitions.Add(new RowDefinition { Height = System.Windows.GridLength.Auto });
                newSubGrid.RowDefinitions.Add(new RowDefinition { Height = System.Windows.GridLength.Auto });

                System.Windows.Controls.Label newLabel = new Label();
                newSubGrid.Children.Add(newLabel);
                newLabel.Background = System.Windows.Media.Brushes.Red;
                    


            }
        }

        private void AddRooms(int roomNumbers)
        {
            //this will probably be a foreach looping through an array of room options, populating each button with the necessary things
            for (int i = 0; i < roomNumbers; i++)
            {
                System.Windows.Controls.Button newBtn = new Button();

                newBtn.Content = "ROOMNAME " + i.ToString();
                newBtn.Name = "Button" + i.ToString();

                LeftStackTop.Children.Add(newBtn);
            }
        }

        private void AddUsers(int connectedUsers)
        {
            for (int i = 0; i < connectedUsers; i++)
            {
                System.Windows.Controls.Button newBtn = new Button();

                newBtn.Content = "USERNAME" + i.ToString();
                newBtn.Name = "Button" + i.ToString();

                LeftStackBottom.Children.Add(newBtn);
            }
        }

        private void SendMessage(string msg)
        {
            NetworkStream clientStream = client.GetStream();

            byte[] buffer = Encoding.UTF8.GetBytes(msg);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();

            // Get TcpServer.response

            // Buffer to store response
            byte[] data = new byte[Constants.BUFFER_SIZE];

            // store ASCII representation
            string responseData = "";

            // Read first bytes
            Int32 bytes = clientStream.Read(data, 0, data.Length);
            responseData = Encoding.ASCII.GetString(data, 0, bytes);

            //messageBox.AppendText(Environment.NewLine + "From Server: " + responseData);
            //messageBox.Dispatcher.Invoke(() => messageBox.AppendText(Environment.NewLine + "From Server: " + responseData));
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(messageBox.Text);
            messageBox.Text = "";
            //messageBox.Dispatcher.Invoke(() => messageBox.Text = "");
        }

        private void FileAttachmentButtonHandler(object sender, RoutedEventArgs e)
        {
            //will initiate a window that will push a file to the server from the client
        }

        private void ImageAttachmentButtonHandler(object sender, RoutedEventArgs e)
        {
            //will initiate a window that will push a file to the server from the client
        }

        private void EmojiAttachmentHandler(object sender, RoutedEventArgs e)
        {
            //https://github.com/shine-calendar/EmojiBox
            //Haven't looked into this but it might be super useful
        }
    }
}
