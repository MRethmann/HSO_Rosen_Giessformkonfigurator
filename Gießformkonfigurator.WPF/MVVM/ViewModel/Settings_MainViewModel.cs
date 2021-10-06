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
        public RelayCommand ToleranceSettingsViewCommand { get; set; }
        public RelayCommand RankingSettingsViewCommand { get; set; }

        public Settings_ApplicationSettingsViewModel ApplicationSettingsVm { get; set; }
        public Settings_ToleranceSettingsViewModel ToleranceSettingsVm { get; set; }
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
            ToleranceSettingsVm = new Settings_ToleranceSettingsViewModel();
            RankingSettingsVm = new Settings_RankingSettingsViewModel();

            CurrentViewAdmin = ApplicationSettingsVm;

            ApplicationSettingsViewCommand = new RelayCommand(o =>
            {
                CurrentViewAdmin = ApplicationSettingsVm;
            });

            ToleranceSettingsViewCommand = new RelayCommand(o =>
            {
                CurrentViewAdmin = ToleranceSettingsVm;
            });

            RankingSettingsViewCommand = new RelayCommand(o =>
            {
                CurrentViewAdmin = RankingSettingsVm;
            });
        }
    }
}
