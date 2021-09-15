//-----------------------------------------------------------------------
// <copyright file="Product_MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using Gießformkonfigurator.WPF.Core;
    using System;
    using System.Windows.Input;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
    using System.Windows;

    class Product_MainViewModel : ObservableObject
    {
        public string description { get; set; }

        public decimal outerDiameter { get; set; }

        public decimal innerDiameter { get; set; }

        public decimal height { get; set; }

        public int drillHoles { get; set; }

        public string material { get; set; }

        public ICommand insertIntoDbCmd { get; set; }

        public RelayCommand Product_DiscViewCmd { get; set; }
        public RelayCommand Product_CupViewCmd { get; set; }

        public Product_DiscViewModel Product_DiscViewModel { get; set; }
        public Product_CupViewModel Product_CupViewModel { get; set; }

        private object _currentView_Product_MainView;

        public object currentView_Product_MainView
        {
            get { return _currentView_Product_MainView; }
            set
            {
                _currentView_Product_MainView = value;
                OnPropertyChanged();
            }
        }

        public Product_MainViewModel()
        {
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateData());
            Product_DiscViewModel = new Product_DiscViewModel();
            Product_CupViewModel = new Product_CupViewModel();

            currentView_Product_MainView = Product_DiscViewModel;

            Product_DiscViewCmd = new RelayCommand(o =>
            {
                currentView_Product_MainView = Product_DiscViewModel;
            });

            Product_CupViewCmd = new RelayCommand(o =>
            {
                currentView_Product_MainView = Product_CupViewModel;
            });
        }

        public void insertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    ProductDisc pd = new ProductDisc() { OuterDiameter = this.outerDiameter, InnerDiameter = this.innerDiameter, Height = this.height, Hc1HoleDiameter = 0.0m, Hc1Diameter = 0.0m, Hc1Holes = 0.0m, Hc2HoleDiameter = 0.0m, Hc2Diameter = 0.0m, Hc2Holes = 0.0m, Hc3HoleDiameter = 0.0m, Hc3Diameter = 0.0m, Hc3Holes = 0.0m };
                    db.ProductDiscs.Add(pd);
                    db.SaveChanges();
                    MessageBox.Show("Produkt erfolgreich hinzugefügt.");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Fehler beim Hinzufügen.");
                }

            }
        }
        private bool validateData()
        {
            return this.description != null
                && this.outerDiameter > 0 && this.outerDiameter < 10000
                && this.innerDiameter > 0 && this.innerDiameter < 10000
                && this.outerDiameter > this.innerDiameter
                && this.height > 0 && this.height < 10000;
        }
    }
}
