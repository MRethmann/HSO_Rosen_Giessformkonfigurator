//-----------------------------------------------------------------------
// <copyright file="Components_CoreView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.View
{
    using Giessformkonfigurator.WPF.MVVM.ViewModel;
    using System.Windows.Controls;
    /// <summary>
    /// Interaktionslogik für InnenkernHinzufuegenView.xaml
    /// </summary>
    public partial class Components_CoreView : UserControl
    {
        public Components_CoreView()
        {
            InitializeComponent();
            DataContext = new Components_CoreViewModel();
        }
    }
}
