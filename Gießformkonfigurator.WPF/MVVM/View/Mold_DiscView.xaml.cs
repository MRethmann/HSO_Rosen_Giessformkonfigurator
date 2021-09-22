//-----------------------------------------------------------------------
// <copyright file="Mold_DiscView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.View
{
    using Gießformkonfigurator.WPF.MVVM.ViewModel;
    using System.Windows;
    using System.Windows.Controls;
    /// <summary>
    /// Interaktionslogik für ScheibeHinzufuegenView.xaml
    /// </summary>
    public partial class Mold_DiscView : UserControl
    {
        public Mold_DiscView()
        {
            InitializeComponent();
            DataContext = new Mold_DiscViewModel();
        }
    }
}
