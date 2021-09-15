using Gießformkonfigurator.WPF.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Gießformkonfigurator.WPF.Core
{
    internal class SearchCommand : ICommand
    {
        #region Constructors

        public SearchCommand(Search_MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private Search_MainViewModel _viewModel;

        #endregion Constructors

        #region ICommand Members

        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.findMatchingMolds();
        }

        #endregion ICommand Members
    }
}
