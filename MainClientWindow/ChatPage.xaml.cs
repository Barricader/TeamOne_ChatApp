using ChatBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatBase.Models;

namespace MainClientWindow
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Page
    {
        
        public List<Message> testMessageList = new List<Message>();
        public List<ChatBase.Models.Room> roomList;
        Client mainclient;

        public Chat()
        {
            InitializeComponent();
            mainclient = (Client)FindResource("client");
            messageBox.KeyDown += mainclient.MessageBoxKeyDown;
            mainclient.MsgReceived += GotMessage;
            DataContext = mainclient;
            roomList = mainclient.rooms;
            GeneratePage();
        }

        private void GotMessage(string msg)
        {
            AddSingleMessage(msg);
            //get roomname and add notification
            //room.NewMessages++;
            /*
            for (int i = 0; i < 5; i++)
            {
                testMessageList.Add(new Message(new User { FirstName = "Test", LastName = "User " + i }, roomList[0], String.Format("Message{0}", i), DateTime.Now));
            }
            */
        }


        private void GeneratePage()
        {
            //int is rooms in db 
            AddRooms();
            //int is connected users
            AddUsers(3);
            //AddMessages(roomList[0]);
        }
        private void AddMessages(ChatBase.Models.Room roomname)
        {
            List<Message> roomMessagesList = new List<Message>();
            var roomMessages = from m in testMessageList
                               where m.OwningRoom == roomname
                               select m;
            foreach (Message m in roomMessages)
            {
                roomMessagesList.Add(m);
            }
            MessagesItemControl.ItemsSource = roomMessagesList;

        }


        private void AddSingleMessage(string message)
        {
            

        }


        private void AddRooms()
        {
            //Roomnames cannot have commas in it.
            //this will probably be a foreach looping through an array of rooms, populating each button with the room name
            //there probably needs to be a notify method that will append a (1) after the roomname
            RoomsListView.ItemsSource = mainclient.rooms;
        }

        private void AddRoom(ChatBase.Models.Room room)
        {
            roomList.Add(room);
            RoomsListView.ItemsSource = roomList;
        }

        private void AddUsers(int connectedUsers)
        {
            for (int i = 0; i < connectedUsers; i++)
            {
                AddSingleUser();
            }
        }

        private void AddSingleUser()
        {
            Button newBtn = new Button()
            {
                Content = "USERNAME",
                Name = "Button"
            };
            LeftStackBottom.Children.Add(newBtn);
        }

        private void SendMessage(string msg)
        {
            Console.WriteLine("stdsag");
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(messageBox.Text);
            AddSingleMessage("Added Message");

        }

        private void FileAttachmentButtonHandler(object sender, RoutedEventArgs e)
        {
            //will initiate a window that will push a file to the server from the client
        }

        private void ImageAttachmentButtonHandler(object sender, RoutedEventArgs e)
        {
            //will initiate a window that will push an image file to the server from the client
        }

        private void EmojiAttachmentHandler(object sender, RoutedEventArgs e)
        {
            //https://github.com/shine-calendar/EmojiBox
            //Haven't looked into this but it might be super useful

            //Researching and testing how to put Emojis in a richtextbox 
            //Continueing to research and test Emojis
            //Testing in another WPF application
            //This emoji thing is taking a lot longer than I thought
        }

        private void AddMessagesButtonHandler(object sender, RoutedEventArgs e)
        {
            //Will ping server for 25 more messages based on button pressed.
        }

        private void LogoutButtonClickHandler(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("LoginPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RoomGenClickHandler(object sender, RoutedEventArgs e)
        {
            AddRoom(new ChatBase.Models.Room(roomNameTextBox.Text));
            //Will add new table to database
        }

        private void ClearRoom()
        {
            MessagesStackPanel.Children.Clear();
        }

        public void MBKeyDown(object sender, KeyEventArgs e)
        {
        }

        private void NotificationMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //Man theres gotta be an easier way to do this.
                Label notificationLabel = sender as Label;
                Grid notificationGrid = notificationLabel.Parent as Grid;
                DockPanel notificationDock = notificationGrid.Parent as DockPanel;

                string roomName = notificationDock.Children.OfType<Button>().FirstOrDefault().Content.ToString();
                foreach (ChatBase.Models.Room r in roomList)
                {
                    if (r.Name == roomName)
                    {
                        r.NewMessages = 0;
                        AddMessages(r);
                    }
                }

            }
        }

        private void SwitchRoomButton(object sender, RoutedEventArgs e)
        {
            Button roomButton = sender as Button;
            string roomName = roomButton.Content.ToString();
            foreach (ChatBase.Models.Room r in roomList)
            {
                if (r.Name == roomName)
                {
                    r.NewMessages = 0;
                    AddMessages(r);
                }

            }
        }

        private void Emoji1ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            messageBox.AppendText("\u263a");
        }

        private void Emoji2ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            messageBox.AppendText("\u2639");
        }

        private void Emoji3ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            messageBox.AppendText("\u2764");
        }

        private void Emoji4ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            messageBox.AppendText("\u2620");
        }
    }
}
