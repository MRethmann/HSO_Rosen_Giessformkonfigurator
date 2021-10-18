//-----------------------------------------------------------------------
// <copyright file="Mold_DiscView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.View
{
    using Giessformkonfigurator.WPF.MVVM.ViewModel;
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
