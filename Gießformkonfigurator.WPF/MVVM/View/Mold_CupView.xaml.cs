//-----------------------------------------------------------------------
// <copyright file="Mold_CupView.xaml.cs" company="PlaceholderCompany">
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
    public partial class Mold_CupView : UserControl
    {
        public Mold_CupView()
        {
            InitializeComponent();
            DataContext = new Mold_CupViewModel();
        }
    }
}
