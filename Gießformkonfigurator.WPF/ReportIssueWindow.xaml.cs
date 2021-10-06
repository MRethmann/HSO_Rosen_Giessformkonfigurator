
using Gießformkonfigurator.WPF.MVVM.ViewModel;
using log4net;
using System.Windows;
using System.Windows.Input;

namespace Gießformkonfigurator.WPF
{
    /// <summary>
    /// Interaktionslogik für ReportIssueView.xaml
    /// </summary>
    public partial class ReportIssueWindow : Window
    {
        MainWindow mainWindow;

        private static readonly ILog log = LogManager.GetLogger(typeof(ReportIssueWindow));

        public ReportIssueWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
        }

        private void SendErrorInformation_Click(object sender, RoutedEventArgs e)
        {
            var errorType = ErrorType_ComboBox.Text.ToString();
            var errorDescription = ErrorDescription_TextBox.Text.ToString();
            log.Warn($"Type: {errorType} --- Description: {errorDescription}");
            this.Close();
        }
    }
}
