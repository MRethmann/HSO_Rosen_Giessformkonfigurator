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

    class ToleranceSettings
    {
        public decimal singleMold_OuterDiameter { get; set; }

        public decimal singleMold_InnerDiameter { get; set; }

        public decimal ring_OuterDiameter { get; set; }

        public decimal ring_InnerDiameter { get; set; }

        public decimal core_OuterDiameter { get; set; }

        public decimal hc_Diameter { get; set; }

        public decimal bolt_Diameter { get; set; }

        public decimal product_OuterDiameter { get; set; }

        public decimal product_InnerDiameter { get; set; }

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
                            this.singleMold_OuterDiameter = (Decimal) dataTable?.Rows[0]["singleMold_OuterDiameter"];
                            this.singleMold_InnerDiameter = (Decimal) dataTable?.Rows[0]["singleMold_InnerDiameter"];
                            this.ring_OuterDiameter = (Decimal) dataTable?.Rows[0]["ring_OuterDiameter"];
                            this.ring_InnerDiameter = (Decimal) dataTable?.Rows[0]["ring_InnerDiameter"];
                            this.core_OuterDiameter = (Decimal) dataTable?.Rows[0]["core_OuterDiameter"];
                            this.hc_Diameter = (Decimal) dataTable?.Rows[0]["hc_Diameter"];
                            this.bolt_Diameter = (Decimal) dataTable?.Rows[0]["bolt_Diameter"];
                            this.product_OuterDiameter = (Decimal) dataTable?.Rows[0]["product_OuterDiameter"];
                            this.product_InnerDiameter = (Decimal) dataTable?.Rows[0]["product_InnerDiameter"];
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Tolerance Settings konnten nicht abgerufen werden!" + ex);
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
