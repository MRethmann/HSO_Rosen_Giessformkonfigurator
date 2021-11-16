//-----------------------------------------------------------------------
// <copyright file="ApplicationSettings.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.Core
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows;
    using log4net;

    public class CombinationSettings
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ToleranceSettings));

        public CombinationSettings()
        {
            this.GetCurrentSettingsFromDb();
        }

        /// <summary>
        /// Gets or sets the outside tolerance for all konus combinations. Used to avoid incorrect combinations based on slightly wrong data. Sometimes the inner konus is bigger than the outer konus, which is physically impossible but caused by wrong data.
        /// </summary>
        public decimal Tolerance_Konus_MAX { get; set; }

        /// <summary>
        /// Gets or sets the inside tolerance for all konus combinations. Declares the acceptable difference between inner konus and outer konus. When the difference is too large, the inside component would move too much.
        /// </summary>
        public decimal Tolerance_Konus_MIN { get; set; }

        /// <summary>
        /// Gets or sets the MAX Tolerance for guide bolts. Declares how much smaller the guide may be. When the difference is too large, the inside component would move too much.
        /// </summary>
        public decimal Tolerance_Flat_MAX { get; set; }

        /// <summary>
        /// Gets or sets the MIN Tolerance for guide bolts. Declares the minimum difference, so the guide bolts fits into the hole.
        /// </summary>
        public decimal Tolerance_Flat_MIN { get; set; }

        public void GetCurrentSettingsFromDb()
        {
            string connString = ConfigurationManager.ConnectionStrings["GießformDB"].ToString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    string query = "SELECT * FROM dbo.CombinationSettings";
                    DataTable dataTable = new DataTable();
                    try
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                            dataAdapter.Fill(dataTable);
                            this.Tolerance_Flat_MAX = (decimal) dataTable?.Rows[0]["Tolerance_Flat_MAX"];
                            this.Tolerance_Flat_MIN = (decimal) dataTable?.Rows[0]["Tolerance_Flat_MIN"];
                            this.Tolerance_Konus_MAX = (decimal) dataTable?.Rows[0]["Tolerance_Konus_MAX"];
                            this.Tolerance_Konus_MIN = (decimal) dataTable?.Rows[0]["Tolerance_Konus_MIN"];
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex);
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
