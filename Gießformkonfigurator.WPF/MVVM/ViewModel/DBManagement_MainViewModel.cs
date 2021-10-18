//-----------------------------------------------------------------------
// <copyright file="DBManagement_MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using Giessformkonfigurator.WPF.Core;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_supportClasses;

    class DBManagement_MainViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DBManagement_MainViewModel"/> class.
        /// </summary>
        public DBManagement_MainViewModel()
        {
            this.DbTableSelection = new ObservableCollection<String>();
            this.DbAttributeSelection = new ObservableCollection<String>();
            this.GetDbTables();
            this.SearchCommand = new RelayCommand(param => this.DatabaseQuery(), param => this.ValidateSearch());
        }

        public string ConnectionString { get; set; } = ConfigurationManager.ConnectionStrings["GießformDB"].ToString();

        public ObservableCollection<string> DbTableSelection { get; set; }

        public ObservableCollection<string> DbAttributeSelection { get; set; }

        private string _SelectedTable;

        public string SelectedTable 
        {
            get
            {
                return this._SelectedTable;
            }

            set
            {
                this._SelectedTable = value;
                this.OnPropertyChanged("selectedTable");
                this.GetTableAttributes();
            }
        }

        private string _SelectedAttribute;

        public string SelectedAttribute
        {
            get
            {
                return this._SelectedAttribute; 
            }

            set
            {
                this._SelectedAttribute = value;
            }
        }

        private string _AttributeValue;

        public string AttributeValue
        {
            get
            {
                return this._AttributeValue;
            }

            set
            {
                this._AttributeValue = value;
            }
        }

        public Visibility IsLoading { get; set; } = Visibility.Hidden;

        public ICommand SearchCommand { get; set; }

        public void GetDbTables()
        {
            this.DbTableSelection.Clear();
            string connString = this.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME NOT IN ('__MigrationHistory', 'ApplicationSettings')";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.DbTableSelection.Add(reader["TABLE_NAME"].ToString());
                        }
                    }
                }
            }
        }

        public void GetTableAttributes()
        {
            this.DbAttributeSelection.Clear();
            string connString = this.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                string table = this.SelectedTable;
                string query = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.DbAttributeSelection.Add(reader["COLUMN_NAME"].ToString());
                        }
                    }
                }
            }
        }

        public void DatabaseQuery()
        {
            string connString = this.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                string table = this.SelectedTable;
                string attribute = this.SelectedAttribute;
                string attributeValue = this.AttributeValue;
                string query = $"SELECT * FROM {table} WHERE {attribute} = '{attributeValue}'";
                DataTable dataTable = new DataTable();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dataTable);
                }
            }
        }

        public bool ValidateSearch()
        {
            if (this.SelectedTable == null || this.SelectedAttribute == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
