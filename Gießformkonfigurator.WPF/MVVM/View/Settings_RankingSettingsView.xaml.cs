//-----------------------------------------------------------------------
// <copyright file="Settings_RankingSettingsView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.View
{
    using Giessformkonfigurator.WPF.MVVM.ViewModel;
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
