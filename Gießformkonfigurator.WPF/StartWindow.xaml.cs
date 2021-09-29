using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gießformkonfigurator.WPF
{
    /// <summary>
    /// Interaktionslogik für StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        MainWindow mainWindow;

        public StartWindow()
        {
            InitializeComponent();
        }

        private void btnOpenModal_Click(object sender, RoutedEventArgs e)
        {
            mainWindow = new MainWindow();
            //mainWindow.setCreatingForm = this;
            mainWindow.Show();
        }


    }
}
