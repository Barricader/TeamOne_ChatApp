﻿using ChatBase;
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
        Client mainclient;
        public List<List<Message>> testRoomMessageList = new List<List<Message>>();
        ChatBase.Models.Room techRoom = new ChatBase.Models.Room("Tech");
        public Chat()
        {
            InitializeComponent();
            GeneratePage();
            mainclient = (Client)FindResource("client");
            messageBox.KeyDown += mainclient.MessageBoxKeyDown;
            mainclient.MsgReceived += GotMessage;
            DataContext = mainclient;

            testRoomMessageList.Add(new List<Message>());
            for (int i = 0; i < 10; i++)
            {
                testRoomMessageList[0].Add(new Message(new User { ScreenName = "test_user", FirstName = "Test ", LastName="User " + i }, techRoom, String.Format("Message{0}", i)));
            }
        }

        //private void CloseWindow() {
        //    main.Close();
        //}

        private void GotMessage(string msg)
        {
            AddSingleMessage(msg);
        }

        private void GeneratePage()
        {
            //int is rooms in db 
            AddRooms(3);
            //int is connected users
            AddUsers(3);
            AddMessages("tech");
        }

        private void AddMessages(string roomname)
        {   
            //foreach message in messages from server(25 loops)
            if (roomname == "all")
            {
            //make request to server to get first 25 messages from each room.
            //append each to the roomMessageList
            //foreachroom
                //roomMessageList.Add(new List<ChatBase.Models.Message>());
                //forloop of 25 messages in array from server
                //because it wont be a jumbled mess of x amount of messages.
                //add to list[i]
            }
            else
            {
                //nothin here yet
                //will get the room name from a the rooms list
                //and then populate it with the messages within that room.
            }
                        
        }


        private void AddSingleMessage(string message)
        {

           /*
            System.Windows.Controls.Grid newGrid = new Grid();
            MessagesStackPanel.Dispatcher.Invoke(() => MessagesStackPanel.Children.Add(newGrid));
            MessagesStackPanel.Children.Add(newGrid);
            newGrid.RowDefinitions.Add(new RowDefinition());
            //nested grid 
            Grid newSubGrid = new Grid();
            newGrid.Children.Add(newSubGrid);
            newSubGrid.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(50) });
            newSubGrid.RowDefinitions.Add(new RowDefinition { Height = System.Windows.GridLength.Auto });

            //Populate the top subrow with profile pic, and username and timestamp

            //This will get the profile picture from the owner of the message 
            Image profPic = new Image();
            BitmapImage profileImage = new BitmapImage(new Uri("test_images/ExampleProfilePic.png", UriKind.Relative));
            profPic.Source = profileImage;
            profPic.Height = 45;
            profPic.Width = 45;
            profPic.HorizontalAlignment = HorizontalAlignment.Left;

            Label userTimestamp = new Label() {
                Content = "USERNAME @ TIMESTAMP",
                Margin = new Thickness(50, 13, 0, 0),
                Foreground = Brushes.DarkGray,
                FontSize = 12
            };
            newSubGrid.Children.Add(profPic);
            newSubGrid.Children.Add(userTimestamp);
            Grid.SetRow(profPic, 0);
            Grid.SetRow(userTimestamp, 0);

            //The content portion will get the type of the message from the message (whether it be an image, message or file) and create a new object accordingly and add it to the 

            //if type is string 
            Label contentLabel = new Label() {
                Background = Brushes.Blue,
                Content = message
            };
            newSubGrid.Children.Add(contentLabel);
            Grid.SetRow(contentLabel, 1);
            //else if type is image
            //not sure how to handle this yet
            //System.Windows.Controls.Image contentImage = new Image();
            //BitmapImage messageImage = new BitmapImage(new Uri("test_images/ExampleProfilePic.png", UriKind.Relative));
            //contentImage.Source = messageImage;
            //newSubGrid.Children.Add(contentImage);
            //Grid.SetRow(contentImage, 1);
            //else if type is file
            //figure that out
            //else
            //System.Windows.Controls.Label nullContentLabel = new Label();
            //contentLabel.Background = System.Windows.Media.Brushes.Blue;
            //contentLabel.Content = "Invalid Content";
            //newSubGrid.Children.Add(contentLabel);
            //Grid.SetRow(contentLabel, 1);
            */    
    }


        private void AddRooms(int roomNumbers)
        {
            //this will probably be a foreach looping through an array of rooms, populating each button with the room name
            //there probably needs to be a notify method that will append a (1) after the roomname
            for (int i = 0; i < roomNumbers; i++)
            {
                Button newBtn = new Button() {
                    Content = "ROOMNAME " + i.ToString(),
                    Name = "Button" + i.ToString()
                };
                LeftStackTop.Children.Add(newBtn);
                Grid.SetColumn(newBtn, 0);
            }
            System.Windows.Controls.Button techRoomButton = new Button();
            techRoomButton.Content = "Technology_TEST";
            LeftStackTop.Children.Add(techRoomButton);
            Grid.SetColumn(techRoomButton, 0);
        }
        
        private void AddRoom(string roomname)
        {
            Button newBtn = new Button() {
                Content = roomname
            };
            LeftStackTop.Children.Add(newBtn);
            Grid.SetColumn(newBtn, 0);
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
            Button newBtn = new Button() {
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
        }

        private void AddMessagesButtonHandler(object sender, RoutedEventArgs e)
        {
            AddMessages("all");
        }

        private void LogoutButtonClickHandler(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("LoginPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RoomGenClickHandler(object sender, RoutedEventArgs e)
        {
            AddRoom(roomNameTextBox.Text);
            //Will add new table to database
            
        }
        
        private void ClearRoom()
        {
            MessagesStackPanel.Children.Clear();
        }

        public void MBKeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}
