//-----------------------------------------------------------------------
// <copyright file="Components_BaseplateView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.View
{
    using Giessformkonfigurator.WPF.MVVM.ViewModel;
    using System.Windows.Controls;
    /// <summary>
    /// Interaktionslogik für GrundplatteHinzufuegenView.xaml
    /// </summary>
    public partial class Components_BaseplateView : UserControl
    {
        public Components_BaseplateView()
        {
            InitializeComponent();
            DataContext = new Components_BaseplateViewModel();
        }
    }
}

