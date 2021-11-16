//-----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.ViewModel
{
    using Giessformkonfigurator.WPF.Core;
    using log4net;

    /// <summary>
    /// Unites all views.
    /// </summary>
    class MainViewModel : ObservableObject
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MainViewModel));

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            this.Search_MainViewModel = new Search_MainViewModel();
            this.Product_MainViewModel = new Product_MainViewModel();
            this.Mold_MainViewModel = new Mold_MainViewModel();
            this.Components_MainViewModel = new Components_MainViewModel();
            this.Settings_MainViewModel = new Settings_MainViewModel();
            this.DBManagement_MainViewModel = new DBManagement_MainViewModel();

            this.CurrentView = this.Search_MainViewModel;
            this.Search_MainViewCmd = new RelayCommand(o =>
            {
                this.CurrentView = this.Search_MainViewModel;
            });

            this.Product_MainViewCmd = new RelayCommand(o =>
            {
                this.CurrentView = this.Product_MainViewModel;
            });

            this.Mold_MainViewCmd = new RelayCommand(o =>
            {
                this.CurrentView = this.Mold_MainViewModel;
            });

            this.Components_MainViewCmd = new RelayCommand(o =>
            {
                this.CurrentView = this.Components_MainViewModel;
            });

            this.Settings_MainViewCmd = new RelayCommand(o =>
            {
                this.CurrentView = this.Settings_MainViewModel;
            });

            this.DBManagement_MainViewCmd = new RelayCommand(o =>
            {
                this.CurrentView = this.DBManagement_MainViewModel;
            });
        }

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

        public object CurrentView
        {
            get
            {
                return _CurrentView;
            }

            set
            {
                this._CurrentView = value;
                this.OnPropertyChanged();
            }
        }

        private object _CurrentView;
    }
}
