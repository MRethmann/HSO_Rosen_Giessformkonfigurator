//-----------------------------------------------------------------------
// <copyright file="Settings_ApplicationSettingsView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.View
{
    using Gießformkonfigurator.WPF.Properties;
    using System.Configuration;
    using System.IO;
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
                    //log4net.GlobalContext.Properties["LogFileName"] = @"E:\Maurice\Downloads"; //log file path 
                    //log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
                }
            }
        }
    }
}
