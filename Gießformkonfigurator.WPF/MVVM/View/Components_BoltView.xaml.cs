//-----------------------------------------------------------------------
// <copyright file="Components_BoltView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.View
{
    using Gießformkonfigurator.WPF.MVVM.ViewModel;
    using System.Windows.Controls;
    /// <summary>
    /// Interaktionslogik für StehbolzenHinzufuegenView.xaml
    /// </summary>
    public partial class Components_BoltView : UserControl
    {
        
        public Components_BoltView()
        {
            InitializeComponent();
            DataContext = new Components_BoltViewModel();
        }
    }
}
