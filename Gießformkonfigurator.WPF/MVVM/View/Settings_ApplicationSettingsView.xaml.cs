//-----------------------------------------------------------------------
// <copyright file="Settings_ApplicationSettingsView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.View
{
    using System.Configuration;
    using System.Windows.Controls;
    /// <summary>
    /// Interaktionslogik für Settings_ApplicationSettingsView.xaml
    /// </summary>
    public partial class Settings_ApplicationSettingsView : UserControl
    {
        public Settings_ApplicationSettingsView()
        {
            InitializeComponent();
        }

        private void LogFilePathSelector_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    CurrentLogFilePath.Text = dialog.SelectedPath.ToString();
                }
            }
        }
    }
}
