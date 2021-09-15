//-----------------------------------------------------------------------
// <copyright file="Mold_DiscView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.View
{
    using System.Windows;
    using System.Windows.Controls;
    /// <summary>
    /// Interaktionslogik für ScheibeHinzufuegenView.xaml
    /// </summary>
    public partial class Mold_DiscView : UserControl
    {
        private string parameter1 = "";
        private string parameter2 = "";
        private string slider = "";
        public Mold_DiscView()
        {
            InitializeComponent();
        }

        private void Hinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            parameter1 = Parameter1TextBox.Text;
            Parameter1Ausgabe.Content = parameter1;

            parameter2 = Parameter2TextBox.Text;
            Parameter2Ausgabe.Content = parameter2;

            slider = SliderTextBox.Text;
            int value = int.Parse(slider);
            SliderAusgabe.Content = value;

            if ((bool)Eins.IsChecked)
            {
                AuswahlAusgabe.Content = "A1";
            }
            else
            {
                AuswahlAusgabe.Content = "A2";
            }

            Parameter1TextBox.Text = "";
            Parameter2TextBox.Text = "";
            SliderTextBox.Text = "0";
        }
    }
}
