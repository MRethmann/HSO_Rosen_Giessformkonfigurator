//-----------------------------------------------------------------------
// <copyright file="DBManagement_MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using Gießformkonfigurator.WPF.Core;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;

    class DBManagement_MainViewModel : ObservableObject
    {
        public ObservableCollection<Object> mGießformenFinal { get; }

        public Visibility IsLoading { get; set; } = Visibility.Hidden;

        public ICommand searchCommand { get; set; }

        public DBManagement_MainViewModel()
        {
            searchCommand = new RelayCommand(param => databaseQuery(), param => validateSearch());
        }

        public void databaseQuery()
        {
            MessageBox.Show("Hi");
        }

        public bool validateSearch()
        {
            return true;
        }
    }
}
