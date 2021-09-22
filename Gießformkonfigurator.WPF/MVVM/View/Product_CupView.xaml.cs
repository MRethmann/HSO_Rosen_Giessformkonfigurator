//-----------------------------------------------------------------------
// <copyright file="Product_CupView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.View
{
    using Gießformkonfigurator.WPF.MVVM.ViewModel;
    using System.Windows.Controls;
    /// <summary>
    /// Interaktionslogik für ProduktCupHinzufuegenView.xaml
    /// </summary>
    public partial class Product_CupView : UserControl
    {
        public Product_CupView()
        {
            InitializeComponent();
            DataContext = new Product_CupViewModel();
        }
    }
}
