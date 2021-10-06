//-----------------------------------------------------------------------
// <copyright file="Settings_RankingSettingsViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Gießformkonfigurator.WPF.Core;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    class Settings_RankingSettingsViewModel : ObservableObject
    {
        public decimal rankingFactorOuterDiameter { get; set; }

        public decimal rankingFactorInnerDiameter { get; set; }

        public decimal rankingFactorBolts { get; set; }

        public RankingSettings rankingSettings { get; set; }

        public ICommand insertIntoDbCmd { get; set; }

        public Settings_RankingSettingsViewModel()
        {
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateData());
            rankingSettings = new RankingSettings();
            this.rankingFactorOuterDiameter = rankingSettings.rankingFactorOuterDiameter;
            this.rankingFactorInnerDiameter = rankingSettings.rankingFactorInnerDiameter;
            this.rankingFactorBolts = rankingSettings.rankingFactorBolts;
        }

        public void insertIntoDb()
        {
            var connString = ConfigurationManager.ConnectionStrings["GießformDB"].ToString();
            if (checkValue(rankingFactorOuterDiameter, 0, 1) && checkValue(rankingFactorInnerDiameter, 0, 1) && checkValue(rankingFactorBolts, 0, 1))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        NumberFormatInfo nfi = new NumberFormatInfo();
                        nfi.NumberDecimalSeparator = ".";
                        string query = $"UPDATE dbo.RankingSettings SET rankingFactorOuterDiameter = {this.rankingFactorOuterDiameter.ToString(nfi)}, rankingFactorInnerDiameter = {this.rankingFactorInnerDiameter.ToString(nfi)}, rankingFactorBolts = {this.rankingFactorBolts.ToString(nfi)}";
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
                MessageBox.Show("Deine Werte liegen außerhalb des erforderlichen Bereichs - bitte prüfen!");
        }

        /// <summary>
        /// Validates if all required fields are filled out and activates the button if true.
        /// </summary>
        /// <returns>True if all required fields are filled.</returns>
        private bool validateData()
        {
            return true;
        }

        private bool checkValue(decimal current, decimal min, decimal max)
        {
            if (current < min || current > max)
                return false;
            else
                return true;
        }
    }
}
