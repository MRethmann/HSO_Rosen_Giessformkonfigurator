//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF
{
    using System.Windows;

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Window _startWindow;
        private Window _loginWindow;

        public MainWindow()
        {
            InitializeComponent();
        }

        public Window startWindow
        {
            get { return _startWindow; }
            set { _startWindow = value; }
        }

        public Window loginWindow
        {
            get { return _loginWindow; }
            set { _loginWindow = value; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (startWindow != null)
                startWindow.Close();
        }

        private void Open_LoginView(object sender, RoutedEventArgs e)
        {
            loginWindow = new loginWindow(this);
            loginWindow.Show();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            create_product.Visibility = Visibility.Hidden;
            create_one_piece_mold.Visibility = Visibility.Hidden;
            create_casting_mold_component.Visibility = Visibility.Hidden;
            Settings.Visibility = Visibility.Hidden;
            database_management.Visibility = Visibility.Hidden;
            AdminLoginButton.IsEnabled = true;
        }
    }
}

