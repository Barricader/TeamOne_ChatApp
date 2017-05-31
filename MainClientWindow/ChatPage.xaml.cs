using ChatBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChatBase.Models;
using System.Threading;
using System.Collections.ObjectModel;

namespace MainClientWindow {
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Page {
        Client mainclient;
        public List<Message> testMessageList = new List<Message>();
        public ObservableCollection<ChatBase.Models.Room> roomList = new ObservableCollection<ChatBase.Models.Room>();

        public List<Message> msgQueue = new List<Message>();
        
        public Chat() {
            InitializeComponent();
            mainclient = (Client)FindResource("client");
            messageBox.KeyDown += mainclient.MessageBoxKeyDown;
            mainclient.MsgReceived += GotMessage;
            RoomGenerationButton.Click += mainclient.RoomGenerationButtonClick;
            DataContext = mainclient;

            mainclient.RoomHandler += AddRoom;
            mainclient.HasRoomEvent += ClearQueue;
            RoomsListView.ItemsSource = roomList;
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

        private void AddRoom(ChatBase.Models.Room room) {

            App.Current.Dispatcher.Invoke(() => roomList.Add(room));
            //RoomsListView.Dispatcher.Invoke(() => RoomsListView.ItemsSource = roomList);

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
            //This emoji thing is taking a lot longer than I thought
        }

        private void AddMessagesButtonHandler(object sender, RoutedEventArgs e) {
            //Will ping server for 25 more messages based on button pressed.
        }

        private void LogoutButtonClickHandler(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new Uri("LoginPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RoomGenClickHandler(object sender, RoutedEventArgs e)
        {
            //AddRoom(new ChatBase.Models.Room(roomNameTextBox.Text));
            //Will add new table to database
        }

        private void ClearRoom() {
            MessagesStackPanel.Children.Clear();
        }

        public void MBKeyDown(object sender, KeyEventArgs e)
        {
        }

        private void NotificationMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                //Man theres gotta be an easier way to do this.
                Label notificationLabel = sender as Label;
                Grid notificationGrid = notificationLabel.Parent as Grid;
                DockPanel notificationDock = notificationGrid.Parent as DockPanel;


                string roomName = notificationDock.Children.OfType<Button>().FirstOrDefault().Content.ToString();

                // TODO: consolidate to one method

                ChatBase.Models.Room temp = null;

                foreach (ChatBase.Models.Room r in mainclient.rooms) {
                    if (r.Name == roomName) {
                        temp = r;
                    }
                }

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

            ChatBase.Models.Room temp = null;

            foreach (ChatBase.Models.Room r in mainclient.rooms) {
                if (r.Name == roomName) {
                    temp = r;
                }
            }

            mainclient.user.CurRoom = temp;

            Console.WriteLine(mainclient.user.CurRoom);

            Console.WriteLine(temp.Name);

            foreach (ChatBase.Models.Room r in roomList) {
                if (r.Name == roomName) {
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
