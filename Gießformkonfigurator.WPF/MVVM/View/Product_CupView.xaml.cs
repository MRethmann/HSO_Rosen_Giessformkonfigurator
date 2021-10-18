//-----------------------------------------------------------------------
// <copyright file="Product_CupView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.View
{
    using Giessformkonfigurator.WPF.MVVM.ViewModel;
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
