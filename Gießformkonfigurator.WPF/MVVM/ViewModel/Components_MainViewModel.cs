//-----------------------------------------------------------------------
// <copyright file="Components_MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using Gießformkonfigurator.WPF.Core;

    class Components_MainViewModel : ObservableObject
    {
        public RelayCommand Components_BaseplateViewCmd { get; set; }
        public RelayCommand Components_InsertPlateViewCmd { get; set; }
        public RelayCommand Components_BoltViewCmd { get; set; }
        public RelayCommand Components_RingViewCmd { get; set; }
        public RelayCommand Components_CoreViewCmd { get; set; }
        public RelayCommand Components_CupformViewCmd { get; set; }

        public Components_BaseplateViewModel Components_BaseplateViewModel { get; set; }
        public Components_InsertPlateViewModel Components_InsertPlateViewModel { get; set; }
        public Components_BoltViewModel Components_BoltViewModel { get; set; }
        public Components_RingViewModel Components_RingViewModel { get; set; }
        public Components_CoreViewModel Components_CoreViewModel { get; set; }
        public Components_CupformViewModel Components_CupformViewModel { get; set; }

        private object _currentView_Components_MainView;

        public object currentView_Components_MainView
        {
            get { return _currentView_Components_MainView; }
            set
            {
                _currentView_Components_MainView = value;
                OnPropertyChanged();
            }
        }

        public Components_MainViewModel()
        {
            Components_BaseplateViewModel = new Components_BaseplateViewModel();
            Components_InsertPlateViewModel = new Components_InsertPlateViewModel();
            Components_BoltViewModel = new Components_BoltViewModel();
            Components_RingViewModel = new Components_RingViewModel();
            Components_CoreViewModel = new Components_CoreViewModel();
            Components_CupformViewModel = new Components_CupformViewModel();

            currentView_Components_MainView = Components_BaseplateViewModel;

            Components_BaseplateViewCmd = new RelayCommand(o =>
            {
                currentView_Components_MainView = Components_BaseplateViewModel;
            });

            Components_InsertPlateViewCmd = new RelayCommand(o => 
            {
                currentView_Components_MainView = Components_InsertPlateViewModel;
            });

            Components_BoltViewCmd = new RelayCommand(o => 
            {
                currentView_Components_MainView = Components_BoltViewModel;
            });

            Components_RingViewCmd = new RelayCommand(o =>
            {
                currentView_Components_MainView = Components_RingViewModel;
            });

            Components_CoreViewCmd = new RelayCommand(o =>
            {
                currentView_Components_MainView = Components_CoreViewModel;
            });

            Components_CupformViewCmd = new RelayCommand(o =>
            {
                currentView_Components_MainView = Components_CupformViewModel;
            });
        }
    }
}
