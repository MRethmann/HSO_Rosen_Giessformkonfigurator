//-----------------------------------------------------------------------
// <copyright file="Settings_MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using Gießformkonfigurator.WPF.Core;
    class Settings_MainViewModel : ObservableObject
    {
        public RelayCommand ApplicationSettingsViewCommand { get; set; }
        public RelayCommand FilterSettingsViewCommand { get; set; }
        public RelayCommand CombinationSettingsViewCommand { get; set; }
        public RelayCommand CompareSettingsViewCommand { get; set; }
        public RelayCommand RankingSettingsViewCommand { get; set; }

        public Settings_ApplicationSettingsViewModel ApplicationSettingsVm { get; set; }
        public Settings_FilterSettingsViewModel FilterSettingsVm { get; set; }
        public Settings_CombinationSettingsViewModel CombinationSettingsVm { get; set; }
        public Settings_CompareSettingsViewModel CompareSettingsVm { get; set; }
        public Settings_RankingSettingsViewModel RankingSettingsVm { get; set; }

        private object _currentViewAdmin;

        public object CurrentViewAdmin
        {
            get { return _currentViewAdmin; }
            set
            {
                _currentViewAdmin = value;
                OnPropertyChanged();
            }
        }

        public Settings_MainViewModel()
        {
            ApplicationSettingsVm = new Settings_ApplicationSettingsViewModel();
            FilterSettingsVm = new Settings_FilterSettingsViewModel();
            CombinationSettingsVm = new Settings_CombinationSettingsViewModel();
            CompareSettingsVm = new Settings_CompareSettingsViewModel();
            RankingSettingsVm = new Settings_RankingSettingsViewModel();

            CurrentViewAdmin = ApplicationSettingsVm;

            ApplicationSettingsViewCommand = new RelayCommand(o =>
            {
                CurrentViewAdmin = ApplicationSettingsVm;
            });

            FilterSettingsViewCommand = new RelayCommand(o =>
            {
                CurrentViewAdmin = FilterSettingsVm;
            });

            CombinationSettingsViewCommand = new RelayCommand(o =>
            {
                CurrentViewAdmin = CombinationSettingsVm;
            });

            CompareSettingsViewCommand = new RelayCommand(o =>
            {
                CurrentViewAdmin = CompareSettingsVm;
            });

            RankingSettingsViewCommand = new RelayCommand(o =>
            {
                CurrentViewAdmin = RankingSettingsVm;
            });
        }
    }
}
