//-----------------------------------------------------------------------
// <copyright file="Components_InsertPlateView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.View
{
    using Gießformkonfigurator.WPF.MVVM.ViewModel;
    using System.Windows;
    using System.Windows.Controls;
    /// <summary>
    /// Interaktionslogik für EinlegeplatteHinzufuegenView.xaml
    /// </summary>
    public partial class Components_InsertPlateView : UserControl
    {
        public Components_InsertPlateView()
        {
            InitializeComponent();
            DataContext = new Components_InsertPlateViewModel();
        }

        private void Hinzufuegen_Click(object sender, RoutedEventArgs e)
        {

        }

     
    }
}
