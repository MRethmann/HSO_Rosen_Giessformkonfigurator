//-----------------------------------------------------------------------
// <copyright file="Components_BaseplateView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.View
{
    using System.Windows.Controls;
    using Giessformkonfigurator.WPF.MVVM.ViewModel;

    /// <summary>
    /// Interaktionslogik für GrundplatteHinzufuegenView.xaml.
    /// </summary>
    public partial class Components_BaseplateView : UserControl
    {
        public Components_BaseplateView()
        {
            this.InitializeComponent();
            this.DataContext = new Components_BaseplateViewModel();
        }
    }
}