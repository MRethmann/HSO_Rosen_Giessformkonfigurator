
using Giessformkonfigurator.WPF.Properties;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace Giessformkonfigurator.WPF
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

            string connectionString = ConfigurationManager.ConnectionStrings["GießformDB"].ToString();
            string dataSource = new SqlConnectionStringBuilder(connectionString).DataSource;
            string initialCatalog = new SqlConnectionStringBuilder(connectionString).InitialCatalog;
            string userId = new SqlConnectionStringBuilder(connectionString).UserID;
            string password = new SqlConnectionStringBuilder(connectionString).Password;

            Server_TextBox.Text = dataSource;
            DB_TextBox.Text = initialCatalog;
            User_TextBox.Text = userId;
            Password_TextBox.Password = password;
        }

        private void btnOpenModal_Click(object sender, RoutedEventArgs e)
        {
            mainWindow = new MainWindow();
            mainWindow.startWindow = this;
            mainWindow.Show();
        }

    }
}
