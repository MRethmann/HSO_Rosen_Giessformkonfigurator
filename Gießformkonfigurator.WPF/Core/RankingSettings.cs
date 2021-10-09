//-----------------------------------------------------------------------
// <copyright file="ApplicationSettings.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.Core
{
    using log4net;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows;

    public class RankingSettings
    {
        public decimal rankingFactorOuterDiameter { get; set; }

        public decimal rankingFactorInnerDiameter { get; set; }

        public decimal rankingFactorBolts { get; set; }

        private static readonly ILog log = LogManager.GetLogger(typeof(ToleranceSettings));

        public RankingSettings()
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
                    string query = "SELECT * FROM dbo.RankingSettings";
                    DataTable dataTable = new DataTable();
                    try
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                            dataAdapter.Fill(dataTable);
                            this.rankingFactorOuterDiameter = (Decimal) dataTable.Rows[0]["rankingFactorOuterDiameter"];
                            this.rankingFactorInnerDiameter = (Decimal) dataTable.Rows[0]["rankingFactorInnerDiameter"];
                            this.rankingFactorBolts = (Decimal) dataTable.Rows[0]["rankingFactorBolts"];
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
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
