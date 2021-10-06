﻿
using Gießformkonfigurator.WPF.Properties;
using System.Configuration;
using System.Data.SqlClient;
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

        private void ConnectionDataChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Settings.Default.DBServer = Server_TextBox.Text;
            Settings.Default.DBName = DB_TextBox.Text;
            Settings.Default.DBUserId = User_TextBox.Text;
            Settings.Default.DBPassword = Password_TextBox.Password;
            Settings.Default.DBConString = $"data source = {Server_TextBox.Text}; initial catalog = {DB_TextBox.Text}; persist security info = True; user id = {User_TextBox.Text}; password = {Password_TextBox.Password}; multipleactiveresultsets = True; application name = EntityFramework";
        }

    }
}
