//-----------------------------------------------------------------------
// <copyright file="DBManagement_MainView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.View
{
    using Giessformkonfigurator.WPF.MVVM.ViewModel;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows;
    using System.Windows.Controls;
    /// <summary>
    /// Interaktionslogik für DBManagement_MainView.xaml
    /// </summary>
    public partial class DBManagement_MainView : UserControl
    {
        public DBManagement_MainView()
        {
            InitializeComponent();
            DataContext = new DBManagement_MainViewModel();
        }

        private void DBQueryBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["GießformDB"].ToString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    string table = selectedTableComboBox?.SelectedItem?.ToString();
                    string attribute = selectedAttributeComboBox?.SelectedItem?.ToString();
                    string selectedOperator = this.selectedOperatorComboBox?.Text;
                    string attributeValue = attributeValueTextBox?.Text;
                    string query = $"SELECT * FROM {table} WHERE {attribute} {selectedOperator} '{attributeValue}'";

                    if (table != null)
                    {
                        if (attribute == null || String.IsNullOrEmpty(attributeValue))
                        {
                            query = $"SELECT * FROM {table}";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bitte Tabelle auswählen!");
                    }

                    DataTable dataTable = new DataTable();

                    try
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            dbQueryOutput.ItemsSource = null;
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                            dataAdapter.Fill(dataTable);
                            dbQueryOutput.ItemsSource = dataTable.DefaultView;
                        }
                    }
                    catch (Exception)
                    {
                        
                    }
                    
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show("Verbindungsfehler!" + ex);
            }
        }

        private void selectedTableComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedAttributeComboBox.SelectedIndex = -1;
        }
    }
}
