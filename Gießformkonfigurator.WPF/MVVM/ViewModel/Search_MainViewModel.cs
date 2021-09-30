//-----------------------------------------------------------------------
// <copyright file="Search_MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
    using Gießformkonfigurator.WPF.MVVM.Model.Logic;
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    class Search_MainViewModel : ObservableObject
    {
        public ObservableCollection<CompareObject> productSearchOutput { get; } = new ObservableCollection<CompareObject>();

        private ProgramLogic programLogic { get; set; }

        public Visibility IsLoading { get; set; } = Visibility.Hidden;

        public string listSize { get; set; }

        public bool searchByProductId { get; set; } = true;

        public ProductDisc productDisc { get; set; } = new ProductDisc();

        public int productId { get; set; }

        private decimal? _FactorPU;

        public decimal? FactorPU 
        {
            get { return _FactorPU; } 
            set
            {
                this._FactorPU = value;
                OnPropertyChanged("FactorPU");
            }
        }

        public ICommand searchCommand { get; set; }

        public Search_MainViewModel()
        {
            searchCommand = new RelayCommand(param => findMatchingMolds(), param => validateSearch());
        }

        public void findMatchingMolds()
        {
            if (this.searchByProductId)
            {
                try
                {
                    using (var db = new GießformDBContext())
                    {
                        this.productDisc = db.ProductDiscs.Find(productId);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Suche fehlgeschlagen. Überprüfe die Db Verbindung!");
                }
            }
            else
            {
                if (this.productDisc.OuterDiameter == 0
                    || this.productDisc.InnerDiameter == 0
                    || this.productDisc.Height == 0)
                    // || productdisc.FactorPU == null
                    // || productdisc.FactorPU == 0
                    // || this.productDisc.BTC == null)
                {
                    MessageBox.Show("Bitte alle Werte ausfüllen!");
                }
            }


            if (productDisc != null)
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                this.IsLoading = Visibility.Visible;

                programLogic = new ProgramLogic(productDisc);

                this.productSearchOutput.Clear();

                foreach (var compareObject in programLogic.finalOutput)
                {
                    this.productSearchOutput.Add(compareObject);
                }

                this.IsLoading = Visibility.Hidden;
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
            }
            else
            {
                MessageBox.Show("Kein Produkt bekannt!");
            }
        }

        public bool validateSearch()
        {
            /*if (productId != 0)
                return true;
            else
                return false;*/
            return true;
        } 
    }
}