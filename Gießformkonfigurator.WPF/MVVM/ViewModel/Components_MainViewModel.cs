//-----------------------------------------------------------------------
// <copyright file="Components_MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.ViewModel
{
    using Giessformkonfigurator.WPF.Core;

    class Components_MainViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Components_MainViewModel"/> class.
        /// </summary>
        public Components_MainViewModel()
        {
            this.Components_BaseplateViewModel = new Components_BaseplateViewModel();
            this.Components_InsertPlateViewModel = new Components_InsertPlateViewModel();
            this.Components_BoltViewModel = new Components_BoltViewModel();
            this.Components_RingViewModel = new Components_RingViewModel();
            this.Components_CoreViewModel = new Components_CoreViewModel();
            this.Components_CupformViewModel = new Components_CupformViewModel();

            this.CurrentView_Components_MainView = this.Components_BaseplateViewModel;

            this.Components_BaseplateViewCmd = new RelayCommand(o =>
            {
                this.CurrentView_Components_MainView = this.Components_BaseplateViewModel;
            });

            this.Components_InsertPlateViewCmd = new RelayCommand(o =>
            {
                this.CurrentView_Components_MainView = this.Components_InsertPlateViewModel;
            });

            this.Components_BoltViewCmd = new RelayCommand(o =>
            {
                this.CurrentView_Components_MainView = this.Components_BoltViewModel;
            });

            this.Components_RingViewCmd = new RelayCommand(o =>
            {
                this.CurrentView_Components_MainView = this.Components_RingViewModel;
            });

            this.Components_CoreViewCmd = new RelayCommand(o =>
            {
                this.CurrentView_Components_MainView = this.Components_CoreViewModel;
            });

            this.Components_CupformViewCmd = new RelayCommand(o =>
            {
                this.CurrentView_Components_MainView = this.Components_CupformViewModel;
            });
        }

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

        private object _CurrentView_Components_MainView;

        public object CurrentView_Components_MainView
        {
            get 
            { 
                return _CurrentView_Components_MainView; 
            }

            set
            {
                this._CurrentView_Components_MainView = value;
                this.OnPropertyChanged();
            }
        }
    }
}