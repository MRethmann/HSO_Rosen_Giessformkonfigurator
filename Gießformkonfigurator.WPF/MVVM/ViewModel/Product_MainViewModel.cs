//-----------------------------------------------------------------------
// <copyright file="Product_MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;

    class Product_MainViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product_MainViewModel"/> class.
        /// </summary>
        public Product_MainViewModel()
        {
            this.InsertIntoDbCmd = new RelayCommand(param => this.InsertIntoDb(), param => this.ValidateData());
            this.Product_DiscViewModel = new Product_DiscViewModel();
            this.Product_CupViewModel = new Product_CupViewModel();

            this.CurrentView_Product_MainView = this.Product_DiscViewModel;

            this.Product_DiscViewCmd = new RelayCommand(o =>
            {
                this.CurrentView_Product_MainView = this.Product_DiscViewModel;
            });

            this.Product_CupViewCmd = new RelayCommand(o =>
            {
                this.CurrentView_Product_MainView = this.Product_CupViewModel;
            });
        }

        public string Description { get; set; }

        public decimal OuterDiameter { get; set; }

        public decimal InnerDiameter { get; set; }

        public decimal Height { get; set; }

        public int DrillHoles { get; set; }

        public string Material { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

        public RelayCommand Product_DiscViewCmd { get; set; }

        public RelayCommand Product_CupViewCmd { get; set; }

        public Product_DiscViewModel Product_DiscViewModel { get; set; }

        public Product_CupViewModel Product_CupViewModel { get; set; }

        private object _CurrentView_Product_MainView;

        public object CurrentView_Product_MainView
        {
            get
            { 
                return this._CurrentView_Product_MainView;
            }

            set
            {
                this._CurrentView_Product_MainView = value;
                this.OnPropertyChanged();
            }
        }

        public void InsertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    ProductDisc productDisc = new ProductDisc() { OuterDiameter = this.OuterDiameter, InnerDiameter = this.InnerDiameter, Height = this.Height, HcHoleDiameter = 0.0m, HcDiameter = 0.0m, HcHoles = 0 };
                    db.ProductDiscs.Add(productDisc);
                    db.SaveChanges();
                    MessageBox.Show("Produkt erfolgreich hinzugefügt.");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Fehler beim Hinzufügen." + e);
                }
            }
        }

        private bool ValidateData()
        {
            return this.Description != null
                && this.OuterDiameter > 0 && this.OuterDiameter < 10000
                && this.InnerDiameter > 0 && this.InnerDiameter < 10000
                && this.OuterDiameter > this.InnerDiameter
                && this.Height > 0 && this.Height < 10000;
        }
    }
}
