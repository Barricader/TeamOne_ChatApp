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
    class WindowsNotification
    {
        private static string title;

        public static string Title
        {
            get { return title; }
            set { title = value; }
        }

        private static string content;

        public static string Content
        {
            get { return content; }
            set { content = value; }
        }

        private static string image;

        public static string Image
        {
            get { return image; }
            set { image = value; }
        }

        private static string logo = "Images/TestLogo.png";

        public static string Logo
        {
            get { return logo; }
        }

        ToastVisual visual = new ToastVisual()
        {
            BindingGeneric = new ToastBindingGeneric()
            {
                Children =
            {
                new AdaptiveText()
                {
                    Text = Title
                },
                new AdaptiveText()
                {
                    Text = Content
                },
                new AdaptiveImage()
                {
                    Source = Image
                }
            },
                AppLogoOverride = new ToastGenericAppLogo()
                {
                    Source = Logo,
                    HintCrop = ToastGenericAppLogoCrop.Circle
                }
            }
        };
    }
}
