using ChatBase;
using System.Windows;

namespace NewChat {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Client client = new Client();

            DataContext = client;

            Closed += client.Window_Closed;
            messageBox.KeyDown += client.MessageBoxKeyDown;

            client.windowHandler += CloseWindow;

            client.Start();
        }

        public void CloseWindow() {
            Close();
        }
    }
}
