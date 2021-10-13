//-----------------------------------------------------------------------
// <copyright file="Settings_RankingSettingsViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Input;
    using Gießformkonfigurator.WPF.Core;

    class Settings_RankingSettingsViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings_RankingSettingsViewModel"/> class.
        /// </summary>
        public Settings_RankingSettingsViewModel()
        {
            this.InsertIntoDbCmd = new RelayCommand(param => this.InsertIntoDb(), param => this.ValidateData());
            this.RankingSettings = new RankingSettings();
            this.RankingFactorOuterDiameter = this.RankingSettings.RankingFactorOuterDiameter;
            this.RankingFactorInnerDiameter = this.RankingSettings.RankingFactorInnerDiameter;
            this.RankingFactorBolts = this.RankingSettings.RankingFactorBolts;
        }

        public decimal RankingFactorOuterDiameter { get; set; }

        public decimal RankingFactorInnerDiameter { get; set; }

        public decimal RankingFactorBolts { get; set; }

        public RankingSettings RankingSettings { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

        public void InsertIntoDb()
        {
            var connString = ConfigurationManager.ConnectionStrings["GießformDB"].ToString();
            if (this.CheckValue(this.RankingFactorOuterDiameter, 0, 1) && this.CheckValue(this.RankingFactorInnerDiameter, 0, 1) && this.CheckValue(this.RankingFactorBolts, 0, 1))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        NumberFormatInfo nfi = new NumberFormatInfo();
                        nfi.NumberDecimalSeparator = ".";
                        string query = $"UPDATE dbo.RankingSettings SET rankingFactorOuterDiameter = {this.RankingFactorOuterDiameter.ToString(nfi)}, rankingFactorInnerDiameter = {this.RankingFactorInnerDiameter.ToString(nfi)}, rankingFactorBolts = {this.RankingFactorBolts.ToString(nfi)}";
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
            else
            {
                MessageBox.Show("Deine Werte liegen außerhalb des erforderlichen Bereichs - bitte prüfen!");
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

        private bool CheckValue(decimal current, decimal min, decimal max)
        {
            if (current < min || current > max)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
