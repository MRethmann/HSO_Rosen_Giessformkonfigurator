//-----------------------------------------------------------------------
// <copyright file="Settings_CompareSettingsViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Gießformkonfigurator.WPF.Core;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    class Settings_ToleranceSettingsViewModel : ObservableObject
    {
        public decimal product_OuterDiameter_MAX { get; set; }

        public decimal product_InnerDiameter_MAX { get; set; }

        public decimal product_OuterDiameter_MIN { get; set; }

        public decimal product_InnerDiameter_MIN { get; set; }

        public decimal hc_Diameter { get; set; }

        public decimal bolt_Diameter { get; set; }

        public ToleranceSettings toleranceSettings { get; set; }

        public ICommand insertIntoDbCmd { get; set; }

        public Settings_ToleranceSettingsViewModel()
        {
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateData());
            toleranceSettings = new ToleranceSettings();
            this.product_InnerDiameter_MAX = toleranceSettings.product_InnerDiameter_MAX;
            this.product_InnerDiameter_MIN = toleranceSettings.product_InnerDiameter_MIN;
            this.product_OuterDiameter_MAX = toleranceSettings.product_OuterDiameter_MAX;
            this.product_OuterDiameter_MIN = toleranceSettings.product_OuterDiameter_MIN;
            this.bolt_Diameter = toleranceSettings.bolt_Diameter;
            this.hc_Diameter = toleranceSettings.hc_Diameter;
        }

        public void insertIntoDb()
        {
            var connString = ConfigurationManager.ConnectionStrings["GießformDB"].ToString();
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    NumberFormatInfo nfi = new NumberFormatInfo();
                    nfi.NumberDecimalSeparator = ".";
                    string query = $"UPDATE dbo.ToleranceSettings SET product_OuterDiameter_MAX = {this.product_OuterDiameter_MAX.ToString(nfi)}, product_InnerDiameter_MAX = {this.product_InnerDiameter_MAX.ToString(nfi)}, product_OuterDiameter_MIN = {this.product_OuterDiameter_MIN.ToString(nfi)}, product_InnerDiameter_MIN = {this.product_InnerDiameter_MIN.ToString(nfi)}, hc_Diameter = {this.hc_Diameter.ToString(nfi)}, bolt_Diameter = {this.bolt_Diameter.ToString(nfi)}";
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

