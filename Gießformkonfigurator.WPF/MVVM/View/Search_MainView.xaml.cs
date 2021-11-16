//-----------------------------------------------------------------------
// <copyright file="Search_MainView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.View
{
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;
    using Giessformkonfigurator.WPF.MVVM.Model.Logic;
    using Giessformkonfigurator.WPF.MVVM.ViewModel;

    /// <summary>
    /// Interaktionslogik für AnsichtZwei.xaml
    /// </summary>
    public partial class Search_MainView : UserControl
    {
        public Search_MainView()
        {
            this.InitializeComponent();
            this.DataContext = new Search_MainViewModel();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= this.TextBox_GotFocus;
        }

        private void NumberValidation(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
