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

    class ToleranceSettings
    {
        public decimal product_OuterDiameter_MAX { get; set; }

        public decimal product_InnerDiameter_MAX { get; set; }

        public decimal product_OuterDiameter_MIN { get; set; }

        public decimal product_InnerDiameter_MIN { get; set; }

        public decimal hc_Diameter { get; set; }

        public decimal bolt_Diameter { get; set; }

        private static readonly ILog log = LogManager.GetLogger(typeof(ToleranceSettings));

        public ToleranceSettings()
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
                    string query = "SELECT * FROM dbo.ToleranceSettings";
                    DataTable dataTable = new DataTable();
                    try
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                            dataAdapter.Fill(dataTable);
                            this.product_OuterDiameter_MAX = (Decimal) dataTable?.Rows[0]["product_OuterDiameter_MAX"];
                            this.product_InnerDiameter_MAX = (Decimal) dataTable?.Rows[0]["product_InnerDiameter_MAX"];
                            this.product_OuterDiameter_MIN = (Decimal) dataTable?.Rows[0]["product_OuterDiameter_MIN"];
                            this.product_InnerDiameter_MIN = (Decimal) dataTable?.Rows[0]["product_InnerDiameter_MIN"];
                            this.hc_Diameter = (Decimal) dataTable?.Rows[0]["hc_Diameter"];
                            this.bolt_Diameter = (Decimal) dataTable?.Rows[0]["bolt_Diameter"];
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
