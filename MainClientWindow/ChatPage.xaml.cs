using ChatBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChatBase.Models;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MainClientWindow
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Page
    {
        Client mainclient;
        public List<Message> testMessageList = new List<Message>();
        public ObservableCollection<Room> roomList = new ObservableCollection<Room>();

        public List<Message> msgQueue = new List<Message>();
        public Chat()
        {
            InitializeComponent();

            mainclient = (Client)FindResource("client");
            DataContext = mainclient;

            // Event listening
            messageBox.KeyDown += mainclient.MessageBoxKeyDown;
            mainclient.MsgReceived += GotMessage;
            RoomGenerationButton.Click += mainclient.RoomGenerationButtonClick;
            mainclient.RoomHandler += AddRoom;
            mainclient.HasRoomEvent += ClearQueue;
            RoomsListView.ItemsSource = roomList;

            GeneratePage();     // TODO: remove this
        }

        /// <summary>
        /// Send all messages in the queue to the user's current room
        /// </summary>
        private void ClearQueue()
        {
            foreach (Message m in msgQueue)
            {
                m.OwningRoom = mainclient.user.CurRoom;
                GotMessage(m);
            }
        }

        /// <summary>
        /// User received a message, display it to the correct room
        /// </summary>
        /// <param name="msg"></param>
        private void GotMessage(Message msg)
        {
            if (msg.OwningRoom == null)
            {
                // Add to a queue if a the user does not belong to any rooms yet
                msgQueue.Add(msg);
            }
            else
            {
                testMessageList.Add(msg);

                if (msg.OwningRoom.Name != mainclient.user.CurRoom.Name)
                {
                    msg.OwningRoom.NewMessages++;
                }
                else
                {
                    AddMessages(msg.OwningRoom);
                }
            }
        }

        private void GeneratePage()
        {
            AddUsers(3);
        }

        private void AddMessages(Room roomname)
        {
            List<Message> roomMessagesList = new List<Message>();
            var roomMessages = from m in testMessageList
                               where m.OwningRoom == roomname
                               select m;
            foreach (Message m in roomMessages)
            {
                roomMessagesList.Add(m);
            }

            MessagesItemControl.Dispatcher.Invoke(() => MessagesItemControl.ItemsSource = roomMessagesList);

        }

        private void AddRoom(Room room)
        {
            Application.Current.Dispatcher.Invoke(() => roomList.Add(room));
        }

        // TODO: remove any useless functions
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

        private void FileAttachmentButtonHandler(object sender, RoutedEventArgs e)
        {
            //will initiate a window that will push a file to the server from the client
            Stream myStream = null;
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a zipfile";
            op.Filter = "Zip File|*.zip";
            if (op.ShowDialog() == true)
            {
                var size = new FileInfo(op.FileName).Length;
                if (size < 4194304)
                {
                    try
                    {
                        if ((myStream = op.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                mainclient.SerializeAndSendFile(myStream, "zip");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }

                }
                else
                {
                    MessageBox.Show("Size must be under 4MB!");
                }

            }
        }

        private void ImageAttachmentButtonHandler(object sender, RoutedEventArgs e)
        {
            //will initiate a window that will push an image file to the server from the client
            Stream myStream = null;
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                var size = new FileInfo(op.FileName).Length;
                if (size < 4194304)
                {
                    try
                    {
                        if ((myStream = op.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                mainclient.SerializeAndSendFile(myStream, "zip");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Failed to open file. Original error: " + ex.Message);
                    }

                }
                else
                {
                    MessageBox.Show("Size must be under 4MB!");
                }
            }
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
            //AddRoom(new ChatBase.Models.Room(roomNameTextBox.Text));
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

                UpdateMessages(roomName);
            }
        }

        private void SwitchRoomButton(object sender, RoutedEventArgs e)
        {
            Button roomButton = sender as Button;
            string roomName = roomButton.Content.ToString();

            UpdateMessages(roomName);
        }

        private void UpdateMessages(string roomName)
        {
            Room temp = null;

            foreach (Room r in mainclient.rooms)
            {
                if (r.Name == roomName)
                {
                    temp = r;
                }
            }

            mainclient.user.CurRoom = temp;

            foreach (Room r in roomList)
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

        private void MessageBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (messageBox.Text.Length == Constants.MAX_MESSAGE_SIZE)
            {
                // TODO: make visible a label that say "Max message size is 180"
                msgError.Visibility = Visibility.Visible;
            }
            else
            {
                msgError.Visibility = Visibility.Hidden;
            }
        }

        private void UserSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("UserSettingsPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
