using ChatBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChatBase.Models;
using System.Threading;

namespace MainClientWindow {
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Page {
        Client mainclient;
        public List<Message> testMessageList = new List<Message>();
        public List<ChatBase.Models.Room> roomList = new List<ChatBase.Models.Room>();

        public List<Message> msgQueue = new List<Message>();
        
        public Chat() {
            InitializeComponent();
            mainclient = (Client)FindResource("client");
            messageBox.KeyDown += mainclient.MessageBoxKeyDown;
            mainclient.MsgReceived += GotMessage;
            DataContext = mainclient;

            //roomList = mainclient.rooms;

            ChatBase.Models.Room techRoom = new ChatBase.Models.Room("Tech");
            ChatBase.Models.Room securityRoom = new ChatBase.Models.Room("Security");
            ChatBase.Models.Room randomRoom = new ChatBase.Models.Room("Random");
            
            mainclient.RoomHandler += AddRoom;
            mainclient.HasRoomEvent += ClearQueue;

            //randomRoom.NewMessages = 3;
            //securityRoom.NewMessages = 15;


            //roomList.Add(techRoom);
            //roomList.Add(securityRoom);
            //roomList.Add(randomRoom);
            //for (int i = 0; i < 10; i++)
            //{
            //    testMessageList.Add(new Message(new User { FirstName = "Test", LastName = "User " + i }, techRoom, String.Format("Message{0}", i), DateTime.Now));
            //}
            //for (int i = 0; i < 5; i++)
            //{
            //    testMessageList.Add(new Message(new User { FirstName = "Test", LastName = "User " + i }, securityRoom, String.Format("Message{0}", i), DateTime.Now));
            //}

            GeneratePage();

        }

        private void ClearQueue() {
            foreach (Message m in msgQueue) {
                m.OwningRoom = mainclient.user.CurRoom;
                GotMessage(m);
            }
        }

        //private void CloseWindow() {
        //    main.Close();
        //}

        private void GotMessage(Message msg) {
            if (msg.OwningRoom == null) {
                msgQueue.Add(msg);
                //msg.OwningRoom = roomList[0];
            }
            else {
                testMessageList.Add(msg);

                if (msg.OwningRoom.Name != mainclient.user.CurRoom.Name) {
                    msg.OwningRoom.NewMessages++;
                }
                else {
                    AddMessages(msg.OwningRoom);
                }
            }
            //get roomname and add notification
            //room.NewMessages++;
        }


        private void GeneratePage() {
            //int is rooms in db 
            //AddRooms();
            //int is connected users
            AddUsers(3);
            //AddMessages(roomList[0]);
        }
        private void AddMessages(ChatBase.Models.Room roomname) {
            List<Message> roomMessagesList = new List<Message>();
            var roomMessages = from m in testMessageList
                               where m.OwningRoom == roomname
                               select m;
            foreach (Message m in roomMessages) {
                roomMessagesList.Add(m);
            }
            //MessagesItemControl.ItemsSource = roomMessagesList;
            MessagesItemControl.Dispatcher.Invoke(() => MessagesItemControl.ItemsSource = roomMessagesList);

        }


        private void AddSingleMessage(Message message) {
            //AddMessage(roomList.IndexOf(mainclient.user.CurRoom));
            //testMessageList.Add(new Message());
        }


        private void AddRooms() {
            //Roomnames cannot have commas in it.
            //this will probably be a foreach looping through an array of rooms, populating each button with the room name
            //there probably needs to be a notify method that will append a (1) after the roomname
            RoomsListView.ItemsSource = roomList;
        }

        private void AddRoom(ChatBase.Models.Room room) {
            roomList.Add(room);
            //RoomsListView.ItemsSource = roomList;
            RoomsListView.Dispatcher.Invoke(() => RoomsListView.ItemsSource = roomList);
        }

        private void AddUsers(int connectedUsers) {
            for (int i = 0; i < connectedUsers; i++) {
                AddSingleUser();
            }
        }

        private void AddSingleUser() {
            Button newBtn = new Button() {
                Content = "USERNAME",
                Name = "Button"
            };
            LeftStackBottom.Children.Add(newBtn);
        }

        private void SendMessage(string msg) {
            Console.WriteLine("stdsag");
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e) {
            SendMessage(messageBox.Text);
            //AddSingleMessage("Added Message");

        }

        private void FileAttachmentButtonHandler(object sender, RoutedEventArgs e) {
            //will initiate a window that will push a file to the server from the client
        }

        private void ImageAttachmentButtonHandler(object sender, RoutedEventArgs e) {
            //will initiate a window that will push an image file to the server from the client
        }

        private void EmojiAttachmentHandler(object sender, RoutedEventArgs e) {
            //https://github.com/shine-calendar/EmojiBox
            //Haven't looked into this but it might be super useful

            //Researching and testing how to put Emojis in a richtextbox 
            //Continueing to research and test Emojis
            //Testing in another WPF application
        }

        private void AddMessagesButtonHandler(object sender, RoutedEventArgs e) {
            //Will ping server for 25 more messages based on button pressed.
        }

        private void LogoutButtonClickHandler(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new Uri("LoginPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RoomGenClickHandler(object sender, RoutedEventArgs e) {
            //AddRoom(roomNameTextBox.Text);
            //Will add new table to database

        }

        private void ClearRoom() {
            MessagesStackPanel.Children.Clear();
        }

        public void MBKeyDown(object sender, KeyEventArgs e) {
        }

        private void RoomsListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            //AddMessages((ChatBase.Models.Room)RoomsListView.SelectedItem);
        }

        private void NotificationMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                //Man theres gotta be an easier way to do this.
                Label notificationLabel = sender as Label;
                Grid notificationGrid = notificationLabel.Parent as Grid;
                DockPanel notificationDock = notificationGrid.Parent as DockPanel;

                string roomName = notificationDock.Children.OfType<Button>().FirstOrDefault().Content.ToString();
                foreach (ChatBase.Models.Room r in roomList) {
                    if (r.Name == roomName) {
                        r.NewMessages = 0;
                        AddMessages(r);
                    }
                }

            }
        }

        private void SwitchRoomButton(object sender, RoutedEventArgs e) {
            Button roomButton = sender as Button;
            string roomName = roomButton.Content.ToString();
            foreach (ChatBase.Models.Room r in roomList) {
                if (r.Name == roomName) {
                    r.NewMessages = 0;
                    AddMessages(r);
                }

            }
        }
    }
}
