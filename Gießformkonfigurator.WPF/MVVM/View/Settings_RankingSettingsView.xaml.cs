//-----------------------------------------------------------------------
// <copyright file="Settings_RankingSettingsView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.View
{
    using Gießformkonfigurator.WPF.MVVM.ViewModel;
    using System.Windows.Controls;
    /// <summary>
    /// Interaktionslogik für RankingSettingsView.xaml
    /// </summary>
    public partial class Settings_RankingSettingsView : UserControl
    {
        public Settings_RankingSettingsView()
        {
            InitializeComponent();
            DataContext = new Settings_RankingSettingsViewModel();
        }
    }
}
