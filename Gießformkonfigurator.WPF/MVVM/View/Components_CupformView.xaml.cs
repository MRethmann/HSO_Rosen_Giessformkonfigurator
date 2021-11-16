//-----------------------------------------------------------------------
// <copyright file="Components_CupformView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.View
{
    using Giessformkonfigurator.WPF.MVVM.ViewModel;
    using System.Windows;
    using System.Windows.Controls;
    /// <summary>
    /// Interaktionslogik für CupHinzufuegenView.xaml
    /// </summary>
    public partial class Components_CupformView : UserControl
    {
        public Components_CupformView()
        {
            InitializeComponent();
            DataContext = new Components_CupformViewModel();
        }

        private void Hinzufuegen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
