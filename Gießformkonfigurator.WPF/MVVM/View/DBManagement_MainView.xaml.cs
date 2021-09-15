//-----------------------------------------------------------------------
// <copyright file="DBManagement_MainView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.View
{
    using Gießformkonfigurator.WPF.MVVM.ViewModel;
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
    }
}
