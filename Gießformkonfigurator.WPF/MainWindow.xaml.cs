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
        Window creatingForm;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (creatingForm != null)
                creatingForm.Close();
        }


        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            create_product.Visibility = Visibility.Hidden;
            create_one_piece_mold.Visibility = Visibility.Hidden;
            create_casting_mold_component.Visibility = Visibility.Hidden;
            Settings.Visibility = Visibility.Hidden;
            database_management.Visibility = Visibility.Hidden;
            Logout.Visibility = Visibility.Collapsed;
            password_label.Visibility = Visibility.Visible;
            password_box.Visibility = Visibility.Visible;
            Admin_Login.Visibility = Visibility.Visible;
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
                create_product.Visibility = Visibility.Visible;
                create_one_piece_mold.Visibility = Visibility.Visible;
                create_casting_mold_component.Visibility = Visibility.Visible;
                Settings.Visibility = Visibility.Visible;
                database_management.Visibility = Visibility.Visible;
                Logout.Visibility = Visibility.Visible;
                password_label.Visibility = Visibility.Hidden;
                password_box.Visibility = Visibility.Hidden;
                Admin_Login.Visibility = Visibility.Collapsed;
            }
        }
        }
    }

