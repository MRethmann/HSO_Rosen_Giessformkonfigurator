//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF
{
    using System.Windows;

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Window _startWindow;
        private Window _loginWindow;
        private Window _reportIssueWindow;

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

        public Window reportIssueWindow
        {
            get { return _reportIssueWindow; }
            set { _reportIssueWindow = value; }
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

        private void Open_ReportIssueView(object sender, RoutedEventArgs e)
        {
            reportIssueWindow = new ReportIssueWindow(this);
            reportIssueWindow.Show();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Admin_Grid.Visibility = Visibility.Collapsed;
            Admin_Logo.Visibility = Visibility.Collapsed;
            Logo.Visibility = Visibility.Visible;
            AdminLoginButton.IsEnabled = true;
        }
    }
}

