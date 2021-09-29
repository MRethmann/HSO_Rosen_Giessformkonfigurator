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
        Window eyForm;

        public loginWindow()
        {
            InitializeComponent();
        }

        public Window SetEyForm
        {
            get { return eyForm; }
            set { eyForm = value; }
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
                testAusgabe.Text = admin_password;
                mainWindow = new MainWindow();
                mainWindow.password_box.Password = admin_password;
                mainWindow.Show();
            }
        }
    }
}
