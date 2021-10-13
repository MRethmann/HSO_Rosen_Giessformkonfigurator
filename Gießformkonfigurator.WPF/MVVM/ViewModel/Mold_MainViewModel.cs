//-----------------------------------------------------------------------
// <copyright file="Mold_MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using Gießformkonfigurator.WPF.Core;

    /// <summary>
    /// Main View which consists of CupView and DiscView.
    /// </summary>
    public class Mold_MainViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mold_MainViewModel"/> class.
        /// </summary>
        public Mold_MainViewModel()
        {
            this.Mold_DiscViewModel = new Mold_DiscViewModel();
            this.Mold_CupViewModel = new Mold_CupViewModel();

            this.CurrentView_Mold_MainView = this.Mold_DiscViewModel;

            this.Mold_DiscViewCmd = new RelayCommand(o =>
            {
                this.CurrentView_Mold_MainView = this.Mold_DiscViewModel;
            });

            this.Mold_CupViewCmd = new RelayCommand(o =>
            {
                this.CurrentView_Mold_MainView = this.Mold_CupViewModel;
            });
        }

        public RelayCommand Mold_DiscViewCmd { get; set; }

        public RelayCommand Mold_CupViewCmd { get; set; }

        public Mold_DiscViewModel Mold_DiscViewModel { get; set; }

        public Mold_CupViewModel Mold_CupViewModel { get; set; }

        private object _CurrentView_Mold_MainView;

        public object CurrentView_Mold_MainView
        {
            get
            {
                return this._CurrentView_Mold_MainView;
            }

            set
            {
                this._CurrentView_Mold_MainView = value;
                this.OnPropertyChanged();
            }
        }
    }
}
