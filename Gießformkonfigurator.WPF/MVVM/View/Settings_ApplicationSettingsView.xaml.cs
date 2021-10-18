//-----------------------------------------------------------------------
// <copyright file="Settings_ApplicationSettingsView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.View
{
    using Giessformkonfigurator.WPF.MVVM.ViewModel;
    using Giessformkonfigurator.WPF.Properties;
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
            DataContext = new Settings_ApplicationSettingsViewModel();
        }

        private void LogFilePathSelector_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    CurrentLogFilePath.Text = dialog.SelectedPath.ToString();
                    Settings.Default.LogFilePath = dialog.SelectedPath.ToString() + "\\GießformkonfiguratorLogFile";
                    log4net.GlobalContext.Properties["LogFileName"] = dialog.SelectedPath.ToString() + "\\GießformkonfiguratorLogFile";
                    log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
                }
            }
        }
    }
}
