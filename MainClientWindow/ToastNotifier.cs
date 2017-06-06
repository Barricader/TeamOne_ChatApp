using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using ToastNotifications.Messages.Core;

namespace MainClientWindow
{
    public class ToastNotifier
    {
        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(

                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);
            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));
            cfg.Dispatcher = Application.Current.Dispatcher;
        });
       
         
        public void test()
        {

            
            notifier.ShowInformation("Test Message");
            notifier.ShowSuccess("Stuff");
            notifier.ShowWarning("stuff warning");
            notifier.ShowError("stuff error");
            notifier.Dispose();
        }
        
    }
}
