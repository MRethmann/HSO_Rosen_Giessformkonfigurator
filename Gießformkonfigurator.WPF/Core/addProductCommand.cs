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
    internal class addProductCommand : ICommand
    {
        #region Constructors

        public addProductCommand(Product_MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private Product_MainViewModel _viewModel;

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
            _viewModel.insertIntoDb();
        }

        #endregion ICommand Members
    }
}
