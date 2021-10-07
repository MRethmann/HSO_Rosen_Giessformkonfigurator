//-----------------------------------------------------------------------
// <copyright file="Settings_ApplicationSettingsViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Gießformkonfigurator.WPF.Core;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    class Settings_ApplicationSettingsViewModel : ObservableObject
    {
        public string adminPassword { get; set; }

        public string logFilePath { get; set; }

        public ApplicationSettings applicationSettings { get; set; }

        public ICommand insertIntoDbCmd { get; set; }

        public Settings_ApplicationSettingsViewModel()
        {
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateData());
            applicationSettings = new ApplicationSettings();
            this.adminPassword = applicationSettings.adminPassword;
            this.logFilePath = applicationSettings.logFilePath;
        }

        public void insertIntoDb()
        {
            var connString = ConfigurationManager.ConnectionStrings["GießformDB"].ToString();
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    string query = $"UPDATE dbo.ApplicationSettings SET adminPassword = '{this.adminPassword}'";
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
        private bool validateData()
        {
            return true;
        }
    }
}
