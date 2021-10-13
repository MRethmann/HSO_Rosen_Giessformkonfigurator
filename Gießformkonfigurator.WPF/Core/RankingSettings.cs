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
    using log4net;

    public class RankingSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RankingSettings"/> class.
        /// </summary>
        public RankingSettings()
        {
            this.GetCurrentSettingsFromDb();
        }

        public decimal RankingFactorOuterDiameter { get; set; }

        public decimal RankingFactorInnerDiameter { get; set; }

        public decimal RankingFactorBolts { get; set; }

        private static readonly ILog log = LogManager.GetLogger(typeof(ToleranceSettings));

        public void GetCurrentSettingsFromDb()
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
                            this.RankingFactorOuterDiameter = (decimal) dataTable.Rows[0]["rankingFactorOuterDiameter"];
                            this.RankingFactorInnerDiameter = (decimal) dataTable.Rows[0]["rankingFactorInnerDiameter"];
                            this.RankingFactorBolts = (decimal) dataTable.Rows[0]["rankingFactorBolts"];
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
