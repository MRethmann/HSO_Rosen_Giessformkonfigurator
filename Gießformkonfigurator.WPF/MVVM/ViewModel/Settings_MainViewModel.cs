//-----------------------------------------------------------------------
// <copyright file="Settings_MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.ViewModel
{
    using Giessformkonfigurator.WPF.Core;

    class Settings_MainViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings_MainViewModel"/> class.
        /// </summary>
        public Settings_MainViewModel()
        {
            this.ApplicationSettingsVm = new Settings_ApplicationSettingsViewModel();
            this.ToleranceSettingsVm = new Settings_ToleranceSettingsViewModel();
            this.RankingSettingsVm = new Settings_RankingSettingsViewModel();

            this.CurrentViewAdmin = this.ApplicationSettingsVm;

            this.ApplicationSettingsViewCommand = new RelayCommand(o =>
            {
                this.CurrentViewAdmin = this.ApplicationSettingsVm;
            });

            this.ToleranceSettingsViewCommand = new RelayCommand(o =>
            {
                this.CurrentViewAdmin = this.ToleranceSettingsVm;
            });

            this.RankingSettingsViewCommand = new RelayCommand(o =>
            {
                this.CurrentViewAdmin = this.RankingSettingsVm;
            });
        }

        public RelayCommand ApplicationSettingsViewCommand { get; set; }

        public RelayCommand ToleranceSettingsViewCommand { get; set; }

        public RelayCommand RankingSettingsViewCommand { get; set; }

        public Settings_ApplicationSettingsViewModel ApplicationSettingsVm { get; set; }

        public Settings_ToleranceSettingsViewModel ToleranceSettingsVm { get; set; }

        public Settings_RankingSettingsViewModel RankingSettingsVm { get; set; }

        private object _CurrentViewAdmin;

        public object CurrentViewAdmin
        {
            get
            { 
                return this._CurrentViewAdmin; 
            }

            set
            {
                this._CurrentViewAdmin = value;
                this.OnPropertyChanged();
            }
        }
    }
}
