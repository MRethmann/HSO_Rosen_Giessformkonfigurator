//-----------------------------------------------------------------------
// <copyright file="Product_DiscView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.View
{
    using Gießformkonfigurator.WPF.MVVM.ViewModel;
    using System.Windows.Controls;

    /// <summary>
    /// Interaktionslogik für ProduktScheibeHinzufuegenView.xaml
    /// </summary>
    public partial class Product_DiscView : UserControl
    {
        public Product_DiscView()
        {
            InitializeComponent();
            DataContext = new Product_DiscViewModel();
        }
    }
}
