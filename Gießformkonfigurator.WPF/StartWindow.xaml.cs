
using System.Windows;

namespace Gießformkonfigurator.WPF
{
    /// <summary>
    /// Interaktionslogik für StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        MainWindow mainWindow;

        public StartWindow()
        {
            InitializeComponent();
        }

        private void btnOpenModal_Click(object sender, RoutedEventArgs e)
        {
            mainWindow = new MainWindow();
            mainWindow.startWindow = this;
            mainWindow.Show();
        }
    }
}
