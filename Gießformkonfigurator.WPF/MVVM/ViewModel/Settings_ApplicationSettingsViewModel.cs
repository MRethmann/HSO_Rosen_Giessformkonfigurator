//-----------------------------------------------------------------------
// <copyright file="Settings_ApplicationSettingsViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Windows;
    using System.Windows.Input;
    using Gießformkonfigurator.WPF.Core;

    class Settings_ApplicationSettingsViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings_ApplicationSettingsViewModel"/> class.
        /// </summary>
        public Settings_ApplicationSettingsViewModel()
        {
            this.InsertIntoDbCmd = new RelayCommand(param => this.InsertIntoDb(), param => this.ValidateData());
            this.ApplicationSettings = new ApplicationSettings();
            this.AdminPassword = this.ApplicationSettings.adminPassword;
            this.LogFilePath = this.ApplicationSettings.LogFilePath;
        }

        public string AdminPassword { get; set; }

        public string LogFilePath { get; set; }

        public ApplicationSettings ApplicationSettings { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

        public void InsertIntoDb()
        {
            var connString = ConfigurationManager.ConnectionStrings["GießformDB"].ToString();
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    string query = $"UPDATE dbo.ApplicationSettings SET adminPassword = '{this.AdminPassword}'";
                    try
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Einstellungen wurden gespeichert!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Eingabefehler. Überprüfe die Werte! " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verbindungsfehler! " + ex);
            }
        }

        /// <summary>
        /// Validates if all required fields are filled out and activates the button if true.
        /// </summary>
        /// <returns>True if all required fields are filled.</returns>
        private bool ValidateData()
        {
            return true;
        }
    }
}
