//-----------------------------------------------------------------------
// <copyright file="Search_MainView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.View
{
    using System.Windows;
    using System.Windows.Controls;
    using Gießformkonfigurator.WPF.MVVM.Model.Logic;
    using Gießformkonfigurator.WPF.MVVM.ViewModel;

    /// <summary>
    /// Interaktionslogik für AnsichtZwei.xaml
    /// </summary>
    public partial class Search_MainView : UserControl
    {
        public Search_MainView()
        {
            InitializeComponent();
            DataContext = new Search_MainViewModel();
        }

    }
}
