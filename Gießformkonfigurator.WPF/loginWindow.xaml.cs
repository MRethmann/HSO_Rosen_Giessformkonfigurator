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

namespace Gießformkonfigurator.WPF
{
    /// <summary>
    /// Interaktionslogik für loginWindow.xaml
    /// </summary>
    public partial class loginWindow : Window
    {
        MainWindow mainWindow;

        public loginWindow (MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
        }

        private void Admin_Login_Click(object sender, RoutedEventArgs e)
        {
            var admin_password = "SEP";

            if (password_box.Password != admin_password)
            {
                MessageBox.Show("Geben sie das richtige Passwort ein");
            }
            else
            {
                mainWindow.create_product.Visibility = Visibility.Visible;
                mainWindow.create_one_piece_mold.Visibility = Visibility.Visible;
                mainWindow.create_casting_mold_component.Visibility = Visibility.Visible;
                mainWindow.Settings.Visibility = Visibility.Visible;
                mainWindow.database_management.Visibility = Visibility.Visible;
                mainWindow.AdminLoginButton.IsEnabled = false;
                mainWindow.AdminLogoutButton.IsEnabled = true;
                this.Close();
            }
        }
    }
}
