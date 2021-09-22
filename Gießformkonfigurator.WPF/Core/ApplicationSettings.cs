//-----------------------------------------------------------------------
// <copyright file="ApplicationSettings.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.Core
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows;

    class ApplicationSettings
    {
        public decimal deviationKonusBaseplateCore { get; set; }

        public decimal deviationKonusBaseplateInsertPlate { get; set; }

        public decimal deviationKonusBaseplateGuideRing { get; set; }

        public decimal deviationKonusInsertPlateCore { get; set; }

        public decimal rankingFactorOuterDiameter { get; set; }

        public decimal rankingFactorInnerDiameter { get; set; }

        public decimal rankingFactorBolts { get; set; }

        public ApplicationSettings()
        {
            getCurrentSettingsFromDb();
        }

        public void getCurrentSettingsFromDb()
        {
            string connString = ConfigurationManager.ConnectionStrings["GießformDB"].ToString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    string query = "SELECT * FROM dbo.ApplicationSettings";
                    DataTable dataTable = new DataTable();
                    try
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                            dataAdapter.Fill(dataTable);
                            this.deviationKonusBaseplateCore = (Decimal) dataTable.Rows[0]["deviationKonusBaseplateCore"];
                            this.deviationKonusBaseplateInsertPlate = (Decimal) dataTable.Rows[0]["deviationKonusBaseplateInsertPlate"];
                            this.deviationKonusBaseplateGuideRing = (Decimal) dataTable.Rows[0]["deviationKonusBaseplateGuideRing"];
                            this.deviationKonusInsertPlateCore = (Decimal) dataTable.Rows[0]["deviationKonusInsertPlateCore"];
                            this.rankingFactorOuterDiameter = (Decimal) dataTable.Rows[0]["rankingFactorOuterDiameter"];
                            this.rankingFactorInnerDiameter = (Decimal) dataTable.Rows[0]["rankingFactorInnerDiameter"];
                            this.rankingFactorBolts = (Decimal) dataTable.Rows[0]["rankingFactorBolts"];
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Application Settings konnten nicht abgerufen werden!" + ex);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB-Verbindungsfehler!" + ex);
            }
        }
    }
}
