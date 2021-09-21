//-----------------------------------------------------------------------
// <copyright file="DBManagement_MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows;
    using System.Windows.Input;

    class DBManagement_MainViewModel : ObservableObject
    {
        public string connectionString { get; set; } = ConfigurationManager.ConnectionStrings["GießformDB"].ToString();

        public List<String> dbTableSelection { get; set; }

        public List<String> dbAttributeSelection { get; set; }

        public string selectedTable 
        {
            get { return selectedTable; }
            set 
            {
                selectedTable = value;
                this.getTableAttributes();
            }
        }

        public string selectedAttribute { get; set; }

        public Visibility IsLoading { get; set; } = Visibility.Hidden;

        public ICommand searchCommand { get; set; }

        public DBManagement_MainViewModel()
        {
            this.dbTableSelection = new List<String>();
            this.getDbTables();
            searchCommand = new RelayCommand(param => databaseQuery(), param => validateSearch());
        }

        public void getDbTables()
        {
            dbTableSelection.Clear();
            string connString = this.connectionString;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                
                string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME != '__MigrationHistory'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.dbTableSelection.Add(reader["TABLE_NAME"].ToString());
                        }
                    }
                }
            }
        }

        public void getTableAttributes()
        {
            string connString = this.connectionString;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                string table = this.selectedTable;
                string query = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.dbAttributeSelection.Add(reader["TABLE_NAME"].ToString());
                        }
                    }
                }
            }
        }

        public void databaseQuery()
        {
            
        }

        public bool validateSearch()
        {
            return true;
        }
    }
}
