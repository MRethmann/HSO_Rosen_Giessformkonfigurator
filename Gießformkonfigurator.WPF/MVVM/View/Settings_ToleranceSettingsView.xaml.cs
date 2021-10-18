//-----------------------------------------------------------------------
// <copyright file="Settings_CompareSettingsView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.View
{
    using Giessformkonfigurator.WPF.MVVM.ViewModel;
    using System.Windows.Controls;
    /// <summary>
    /// Interaktionslogik für Settings_CompareSettingsView.xaml
    /// </summary>
    public partial class Settings_ToleranceSettingsView : UserControl
    {
        public Settings_ToleranceSettingsView()
        {
            InitializeComponent();
            DataContext = new Settings_ToleranceSettingsViewModel();
        }
    }
}
