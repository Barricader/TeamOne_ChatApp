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
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Page
    {
        public Chat()
        {
            InitializeComponent();
            GeneratePage();
        }
        private void GeneratePage()
        {
            //int is rooms in db 
            AddRooms(3);
            //int is connected users
            AddUsers(3);
            AddMessages();
        }

        private void AddMessages()
        {
            //foreach message in messages from server(25 loops)
            for (int i = 0; i < 25; i++)
            {
                AddSingleMessage("Single Message");
            }
        }

        private void AddSingleMessage(string message)
        {
            System.Windows.Controls.Grid newGrid = new Grid();
            MessagesStackPanel.Children.Add(newGrid);
            newGrid.RowDefinitions.Add(new RowDefinition());
            //nested grid 
            System.Windows.Controls.Grid newSubGrid = new Grid();
            newGrid.Children.Add(newSubGrid);
            newSubGrid.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(50) });
            newSubGrid.RowDefinitions.Add(new RowDefinition { Height = System.Windows.GridLength.Auto });


            //Populate the top subrow with profile pic, and username and timestamp

            //This will get the profile picture from the owner of the message 
            System.Windows.Controls.Image profPic = new Image();
            BitmapImage profileImage = new BitmapImage(new Uri("test_images/ExampleProfilePic.png", UriKind.Relative));
            profPic.Source = profileImage;
            profPic.Height = 45;
            profPic.Width = 45;
            profPic.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

            System.Windows.Controls.Label userTimestamp = new Label();
            userTimestamp.Content = "USERNAME @ TIMESTAMP";
            userTimestamp.Margin = new System.Windows.Thickness(50, 13, 0, 0);
            userTimestamp.Foreground = System.Windows.Media.Brushes.DarkGray;
            userTimestamp.FontSize = 12;

            newSubGrid.Children.Add(profPic);
            newSubGrid.Children.Add(userTimestamp);
            Grid.SetRow(profPic, 0);
            Grid.SetRow(userTimestamp, 0);

            //The content portion will get the type of the message from the message (whether it be an image, message or file) and create a new object accordingly and add it to the 

            //if type is string 
            System.Windows.Controls.Label contentLabel = new Label();
            contentLabel.Background = System.Windows.Media.Brushes.Blue;
            contentLabel.Content = message;
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
        }

        private void AddRooms(int roomNumbers)
        {
            //this will probably be a foreach looping through an array of rooms, populating each button with the room name
            //there probably needs to be a notify method that will append a (1) after the roomname
            for (int i = 0; i < roomNumbers; i++)
            {
                System.Windows.Controls.Button newBtn = new Button();

                newBtn.Content = "ROOMNAME " + i.ToString();
                newBtn.Name = "Button" + i.ToString();

                LeftStackTop.Children.Add(newBtn);
                Grid.SetColumn(newBtn, 0);
            }
        }
        
        private void AddRoom(string roomname)
        {
            System.Windows.Controls.Button newBtn = new Button();

            newBtn.Content = roomname;

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
            System.Windows.Controls.Button newBtn = new Button();

            newBtn.Content = "USERNAME";
            newBtn.Name = "Button";

            LeftStackBottom.Children.Add(newBtn);
        }

        private void SendMessage(string msg)
        {
            
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(messageBox.Text);

            messageBox.Dispatcher.Invoke(() => messageBox.Text = "");
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


        }

        private void AddMessagesButtonHandler(object sender, RoutedEventArgs e)
        {
            AddMessages();
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
    }
}
