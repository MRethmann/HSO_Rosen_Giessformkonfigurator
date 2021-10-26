//-----------------------------------------------------------------------
// <copyright file="Settings_CompareSettingsView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.View
{
    using System.Windows.Controls;
    using Giessformkonfigurator.WPF.MVVM.ViewModel;

    /// <summary>
    /// Interaktionslogik für Settings_CompareSettingsView.xaml
    /// </summary>
    public partial class Settings_CombinationSettingsView : UserControl
    {
        public Settings_CombinationSettingsView()
        {
            this.InitializeComponent();
            this.DataContext = new Settings_CombinationSettingsViewModel();
        }
    }
}
