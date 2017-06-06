using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.QueryStringDotNET;
using Windows.Data.Xml.Dom;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Windows.UI.Notifications;

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
   public  class WindowsNotification
    {
        
        
        public WindowsNotification()
        {
            

        
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText03);
            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
            for (int i = 0; i < stringElements.Length; i++)
            {
                stringElements[i].AppendChild(toastXml.CreateTextNode("Line " + i));
            }
            string imagePath = "file:///" + Path.GetFullPath("/Images/TestLogo.png");
            XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
            imageElements[0].Attributes.GetNamedItem("xrc").NodeValue = imagePath;
            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier("one").Show(toast);



        }

        

        
    
    }




    

}









