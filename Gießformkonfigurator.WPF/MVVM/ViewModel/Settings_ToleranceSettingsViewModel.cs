//-----------------------------------------------------------------------
// <copyright file="Settings_CompareSettingsViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Input;
    using Gießformkonfigurator.WPF.Core;

    class Settings_ToleranceSettingsViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings_ToleranceSettingsViewModel"/> class.
        /// </summary>
        public Settings_ToleranceSettingsViewModel()
        {
            this.InsertIntoDbCmd = new RelayCommand(param => this.InsertIntoDb(), param => this.ValidateData());
            this.ToleranceSettings = new ToleranceSettings();
            this.Product_InnerDiameter_MAX = this.ToleranceSettings.product_InnerDiameter_MAX;
            this.Product_InnerDiameter_MIN = this.ToleranceSettings.product_InnerDiameter_MIN;
            this.Product_OuterDiameter_MAX = this.ToleranceSettings.product_OuterDiameter_MAX;
            this.Product_OuterDiameter_MIN = this.ToleranceSettings.product_OuterDiameter_MIN;
            this.Bolt_Diameter = this.ToleranceSettings.bolt_Diameter;
            this.Hc_Diameter = this.ToleranceSettings.hc_Diameter;
        }

        public decimal Product_OuterDiameter_MAX { get; set; }

        public decimal Product_InnerDiameter_MAX { get; set; }

        public decimal Product_OuterDiameter_MIN { get; set; }

        public decimal Product_InnerDiameter_MIN { get; set; }

        public decimal Hc_Diameter { get; set; }

        public decimal Bolt_Diameter { get; set; }

        public ToleranceSettings ToleranceSettings { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

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
                    string query = $"UPDATE dbo.ToleranceSettings SET product_OuterDiameter_MAX = {this.Product_OuterDiameter_MAX.ToString(nfi)}, product_InnerDiameter_MAX = {this.Product_InnerDiameter_MAX.ToString(nfi)}, product_OuterDiameter_MIN = {this.Product_OuterDiameter_MIN.ToString(nfi)}, product_InnerDiameter_MIN = {this.Product_InnerDiameter_MIN.ToString(nfi)}, hc_Diameter = {this.Hc_Diameter.ToString(nfi)}, bolt_Diameter = {this.Bolt_Diameter.ToString(nfi)}";
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