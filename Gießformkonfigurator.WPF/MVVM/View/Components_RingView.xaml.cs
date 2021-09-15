//-----------------------------------------------------------------------
// <copyright file="Components_RingView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.View
{
    using Gießformkonfigurator.WPF.MVVM.ViewModel;
    using System.Windows.Controls;

    /// <summary>
    /// Interaktionslogik für RingHinzufuegenView.xaml
    /// </summary>
    public partial class Components_RingView : UserControl
    {
        public Components_RingView()
        {
            InitializeComponent();
            DataContext = new Components_RingViewModel();

        }
    }
}

  