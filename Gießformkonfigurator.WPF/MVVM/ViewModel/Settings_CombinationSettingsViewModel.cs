//-----------------------------------------------------------------------
// <copyright file="Settings_CompareSettingsViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.ViewModel
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Input;
    using Giessformkonfigurator.WPF.Core;

    class Settings_CombinationSettingsViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings_CombinationSettingsViewModel"/> class.
        /// </summary>
        public Settings_CombinationSettingsViewModel()
        {
            this.InsertIntoDbCmd = new RelayCommand(param => this.InsertIntoDb(), param => this.ValidateData());
            this.CombinationSettings = new CombinationSettings();
            this.Tolerance_Konus_MAX = this.CombinationSettings.Tolerance_Konus_MAX;
            this.Tolerance_Konus_MIN = this.CombinationSettings.Tolerance_Konus_MIN;
            this.Tolerance_Flat_MAX = this.CombinationSettings.Tolerance_Flat_MAX;
            this.Tolerance_Flat_MIN = this.CombinationSettings.Tolerance_Flat_MIN;
        }

        public CombinationSettings CombinationSettings { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

        public decimal Tolerance_Konus_MAX { get; set; }

        public decimal Tolerance_Konus_MIN { get; set; }

        public decimal Tolerance_Flat_MAX { get; set; }

        public decimal Tolerance_Flat_MIN { get; set; }

        public void InsertIntoDb()
        {
            var connString = ConfigurationManager.ConnectionStrings["GießformDB"].ToString();
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    NumberFormatInfo nfi = new NumberFormatInfo();
                    nfi.NumberDecimalSeparator = ".";
                    string query = $"UPDATE dbo.CombinationSettings SET Tolerance_Konus_MAX = {this.Tolerance_Konus_MAX.ToString(nfi)}, Tolerance_Konus_MIN = {this.Tolerance_Konus_MIN.ToString(nfi)}, Tolerance_Flat_MAX = {this.Tolerance_Flat_MAX.ToString(nfi)}, Tolerance_Flat_MIN = {this.Tolerance_Flat_MIN.ToString(nfi)}";
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