//-----------------------------------------------------------------------
// <copyright file="Settings_CombinationSettingsView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.View
{
    using Gießformkonfigurator.WPF.MVVM.ViewModel;
    using System.Windows.Controls;
    /// <summary>
    /// Interaktionslogik für Settings_CombinationSettingsView.xaml
    /// </summary>
    public partial class Settings_CombinationSettingsView : UserControl
    {
        public Settings_CombinationSettingsView()
        {
            InitializeComponent();
            DataContext = new Settings_CombinationSettingsViewModel();
        }
    }
}
