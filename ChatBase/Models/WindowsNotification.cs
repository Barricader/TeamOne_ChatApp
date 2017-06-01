using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.QueryStringDotNET;


namespace ChatBase.Models
{
    /*
     * Installation requires use of Package Manager Console 
     * 
     * go to Tools > Nu Package Manager > Nu Package Manager Console
     * type in command dispalyed on links below 
     * 
     * Links to NuGet Packages:
     * Microsoft.tool.uwp.Notifications: 
     * For generating toast payloads instead of raw xml https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications/
     * 
     * QueryString.net: 
     * Generating and parsing query strings with C# https://www.nuget.org/packages/QueryString.NET/
     * 
     * Installing NuGet Packages:
     * 
     * go to Nu Package manager > Manage NuGet Packages for solution 
     * click the Microsoft.Toolkit.UWP.Notifications package 
     * on the right there should be multiple check boxes
     * 
     * make sure the version there is a number inside the version column
     * if there aren't any, click the check box next to Project that is missing a version number
     * after selecting the project click the install button
     * 
     * repeat this process for the other package
     * 
     */
    public class WindowsNotification
    {
        //public WindowsNotification(int RoomID, string MessageTitle, string MessageContent, string ImageofSender)
        //{
        //    ConversationID = RoomID;
        //    Title = MessageTitle;
        //    Content = MessageContent;
        //    ProfilePic = ImageofSender;

        //    public ToastVisual visual = new ToastVisual()
        //    {
        //        BindingGeneric = new ToastBindingGeneric()
        //        {
        //            Children =
        //    {
        //        new AdaptiveText()
        //        {
        //            Text = Title
        //        },
        //        new AdaptiveText()
        //        {
        //            Text = Content
        //        },
        //        new AdaptiveImage()
        //        {
        //            Source = ProfilePic
        //        }
        //    },
        //            AppLogoOverride = new ToastGenericAppLogo()
        //            {
        //                Source = Logo,
        //                HintCrop = ToastGenericAppLogoCrop.Circle
        //            }
        //        }
        //    };
        //ToastActionsCustom actions = new ToastActionsCustom()
        //{
        //    Inputs =
        //    {
        //        new ToastTextBox("tbreply")
        //        {
        //            PlaceholderContent = "Type a repsonse"
        //        }
        //    },
        //    Buttons =
        //    {
        //        new ToastButton("Reply", new QueryString()
        //        {
        //            {"action","reply" },
        //            {"conversationId", ConversationID.ToString() }
        //        }.ToString())
        //        {
        //            ActivationType = ToastActivationType.Background,
        //            ImageUri = "Assets/Reply.png",
        //            TextBoxId = "tbReply"
        //        },
        //        new ToastButton("View", new QueryString()
        //        {
        //            {"action","viewImage"},
        //            {"imageUrl", ProfilePic}

        //        }.ToString())
        //    }
        //};


        //ToastContent toastContent = new ToastContent()
        //{
        //    Visual = visual,
        //    Actions = actions,
        //    Launch = new QueryString()
        //        {
        //            {"action","viewConversation" },
        //            {"conversationId", ConversationId.Tostring() }
        //        }

        //};


        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private string profilePic = "/NotificationImages/TestProfileImage.png";

        public string ProfilePic
        {
            get { return profilePic; }
            set { profilePic = value; }
        }

        private string logo = "/NotificationImages/Logo.png";

        public string Logo
        {
            get { return logo; }
        }
        private int conversationID;

        public int ConversationID
        {
            get { return conversationID; }
            set { conversationID = value; }
        }








    }
}




