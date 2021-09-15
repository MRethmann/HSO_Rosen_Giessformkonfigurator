//-----------------------------------------------------------------------
// <copyright file="Mold_MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using Gießformkonfigurator.WPF.Core;

    class Mold_MainViewModel : ObservableObject
    {
        public RelayCommand Mold_DiscViewCmd { get; set; }
        public RelayCommand Mold_CupViewCmd { get; set; }
        public Mold_DiscViewModel Mold_DiscViewModel { get; set; }
        public Mold_CupViewModel Mold_CupViewModel { get; set; }

        private object _currentView_Mold_MainView;
        public object currentView_Mold_MainView
        {
            get { return _currentView_Mold_MainView; }
            set
            {
                _currentView_Mold_MainView = value;
                OnPropertyChanged();
            }
        }

        public Mold_MainViewModel()
        {
            Mold_DiscViewModel = new Mold_DiscViewModel();
            Mold_CupViewModel = new Mold_CupViewModel();

            currentView_Mold_MainView = Mold_DiscViewModel;

            Mold_DiscViewCmd = new RelayCommand(o =>
            {
                currentView_Mold_MainView = Mold_DiscViewModel;
            });

            Mold_CupViewCmd = new RelayCommand(o =>
            {
                currentView_Mold_MainView = Mold_CupViewModel;
            });
        }
    }
}
