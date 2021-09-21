//-----------------------------------------------------------------------
// <copyright file="Settings_CombinationSettingsViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Gießformkonfigurator.WPF.Core;
using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    class Settings_CombinationSettingsViewModel : ObservableObject
    {
        public decimal deviationKonusBaseplateGuideRing { get; set; }
        public decimal deviationKonusBaseplateInsertPlate { get; set; }
        public decimal deviationKonusBaseplateCore { get; set; }
        public decimal deviationKonusInsertPlateCore { get; set; }
        public ApplicationSettings applicationSettings { get; set; }

        public ICommand insertIntoDbCmd { get; set; }

        public Settings_CombinationSettingsViewModel()
        {
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateData());
            applicationSettings = new ApplicationSettings();
            this.deviationKonusBaseplateGuideRing = applicationSettings.deviationKonusBaseplateGuideRing;
            this.deviationKonusBaseplateInsertPlate = applicationSettings.deviationKonusBaseplateInsertPlate;
            this.deviationKonusBaseplateCore = applicationSettings.deviationKonusBaseplateCore;
            this.deviationKonusInsertPlateCore = applicationSettings.deviationKonusInsertPlateCore;
        }

        public void insertIntoDb()
        {
            var connString = ConfigurationManager.ConnectionStrings["GießformDB"].ToString();
            if (checkValue(deviationKonusInsertPlateCore, 0, 100) && checkValue(deviationKonusBaseplateCore, 0, 100) && checkValue(deviationKonusBaseplateInsertPlate, 0, 100) && checkValue(deviationKonusBaseplateGuideRing, 0, 100))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        NumberFormatInfo nfi = new NumberFormatInfo();
                        nfi.NumberDecimalSeparator = ".";
                        string query = $"UPDATE dbo.ApplicationSettings SET deviationKonusBaseplateCore = {this.deviationKonusBaseplateCore.ToString(nfi)}, deviationKonusBaseplateGuideRing = {this.deviationKonusBaseplateGuideRing.ToString(nfi)}, deviationKonusBaseplateInsertPlate = {this.deviationKonusBaseplateInsertPlate.ToString(nfi)}, deviationKonusInsertPlateCore = {this.deviationKonusInsertPlateCore.ToString(nfi)};";
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
