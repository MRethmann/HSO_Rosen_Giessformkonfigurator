//-----------------------------------------------------------------------
// <copyright file="Components_BoltView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.View
{
    using Giessformkonfigurator.WPF.MVVM.ViewModel;
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
