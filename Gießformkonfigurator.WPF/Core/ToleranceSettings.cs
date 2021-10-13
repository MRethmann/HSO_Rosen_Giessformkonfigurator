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

    public class ToleranceSettings
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ToleranceSettings));

        public ToleranceSettings()
        {
            this.GetCurrentSettingsFromDb();
        }

        public decimal Product_OuterDiameter_MAX { get; set; }

        public decimal Product_InnerDiameter_MAX { get; set; }

        public decimal Product_OuterDiameter_MIN { get; set; }

        public decimal Product_InnerDiameter_MIN { get; set; }

        public decimal Hc_Diameter { get; set; }

        public decimal Bolt_Diameter { get; set; }

        public void GetCurrentSettingsFromDb()
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
                            this.Product_OuterDiameter_MAX = (decimal) dataTable?.Rows[0]["product_OuterDiameter_MAX"];
                            this.Product_InnerDiameter_MAX = (decimal) dataTable?.Rows[0]["product_InnerDiameter_MAX"];
                            this.Product_OuterDiameter_MIN = (decimal) dataTable?.Rows[0]["product_OuterDiameter_MIN"];
                            this.Product_InnerDiameter_MIN = (decimal) dataTable?.Rows[0]["product_InnerDiameter_MIN"];
                            this.Hc_Diameter = (decimal) dataTable?.Rows[0]["hc_Diameter"];
                            this.Bolt_Diameter = (decimal) dataTable?.Rows[0]["bolt_Diameter"];
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
