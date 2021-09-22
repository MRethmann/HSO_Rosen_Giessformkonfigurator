//-----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using Gießformkonfigurator.WPF.Core;

    class MainViewModel : ObservableObject
    {
        public RelayCommand Mold_MainViewCmd { get; set; }
        public RelayCommand Product_MainViewCmd { get; set; }
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand Search_MainViewCmd { get; set; }
        public RelayCommand Components_MainViewCmd { get; set; }
        public RelayCommand Settings_MainViewCmd { get; set; }
        public RelayCommand DBManagement_MainViewCmd { get; set; }

        public Mold_MainViewModel Mold_MainViewModel { get; set; }
        public Product_MainViewModel Product_MainViewModel { get; set; }
        public Search_MainViewModel Search_MainViewModel { get; set; }
        public Components_MainViewModel Components_MainViewModel { get; set; }
        public Settings_MainViewModel Settings_MainViewModel { get; set; }
        public DBManagement_MainViewModel DBManagement_MainViewModel { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            Search_MainViewModel = new Search_MainViewModel();
            Product_MainViewModel = new Product_MainViewModel();
            Mold_MainViewModel = new Mold_MainViewModel();
            Components_MainViewModel = new Components_MainViewModel();
            Settings_MainViewModel = new Settings_MainViewModel();
            DBManagement_MainViewModel = new DBManagement_MainViewModel();

            CurrentView = Search_MainViewModel;

            Search_MainViewCmd = new RelayCommand(o =>
            {
                CurrentView = Search_MainViewModel;
            });

            Product_MainViewCmd = new RelayCommand(o =>
            {
                CurrentView = Product_MainViewModel;
            });

            Mold_MainViewCmd = new RelayCommand(o =>
            {
                CurrentView = Mold_MainViewModel;
            });

            Components_MainViewCmd = new RelayCommand(o =>
            {
                CurrentView = Components_MainViewModel;
            });

            Settings_MainViewCmd = new RelayCommand(o =>
            {
                CurrentView = Settings_MainViewModel;
            });

            DBManagement_MainViewCmd = new RelayCommand(o =>
            {
                CurrentView = DBManagement_MainViewModel;
            });
        }
    }
}
