using Giessformkonfigurator.WPF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Giessformkonfigurator.WPF
{
    /// <summary>
    /// Interaktionslogik für loginWindow.xaml
    /// </summary>
    public partial class loginWindow : Window
    {
        MainWindow mainWindow;

        ApplicationSettings applicationSettings;

        public loginWindow (MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            applicationSettings = new ApplicationSettings();
            InitializeComponent();
        }

        private void Admin_Login_Click(object sender, RoutedEventArgs e)
        {
            var admin_password = applicationSettings.AdminPassword;

            if (password_box.Password != admin_password)
            {
                MessageBox.Show("Geben sie das richtige Passwort ein");
                password_box.Password = "";
            }
            else
            {
                password_box.Password = "";
                mainWindow.Admin_Grid.Visibility = Visibility.Visible;
                mainWindow.Admin_Logo.Visibility = Visibility.Visible;
                mainWindow.Logo.Visibility = Visibility.Collapsed;
                mainWindow.AdminLoginButton.IsEnabled = false;
                mainWindow.AdminLogoutButton.IsEnabled = true;
                this.Close();
            }
        }
    }
}
