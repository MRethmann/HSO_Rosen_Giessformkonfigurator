//-----------------------------------------------------------------------
// <copyright file="ApplicationSettings.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.Core
{
    using Gießformkonfigurator.WPF.Properties;
    using log4net;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Windows;

    public class ApplicationSettings
    {
        public string adminPassword { get; set; }

        public string LogFilePath { get; set; }

        private static readonly ILog log = LogManager.GetLogger(typeof(ToleranceSettings));

        public ApplicationSettings()
        {
            getCurrentSettingsFromDb();
            this.LogFilePath = Settings.Default.LogFilePath;
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
                            this.adminPassword = dataTable?.Rows[0]["adminPassword"].ToString();
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
