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

        private SearchJob programLogic { get; set; }

        public Visibility IsLoading { get; set; } = Visibility.Hidden;

        /// <summary>
        /// Property determines the search method. 
        /// True = Search via SAP number. 
        /// False = Search by entering product parameters manually.
        /// </summary>
        public bool searchByProductId { get; set; } = true;

        /// <summary>
        /// Property determines the product type in the GUI via Binding. 
        /// True = ProductDisc. 
        /// False = ProductCup.
        /// </summary>
        public bool searchTypeProduct { get; set; } = true;

        public ProductDisc productDisc { get; set; }

        public ProductCup productCup { get; set; }

        public decimal OuterDiameter { get; set; }

        public decimal InnerDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal? HcDiameter { get; set; }

        public int? HcHoles { get; set; }

        public decimal? HcHoleDiameter { get; set; }

        public decimal? FactorPU { get; set; }

        public string BTC { get; set; }

        public Product product { get; set; }

        public int productId { get; set; }

        public ICommand searchCommand { get; set; }

        public Search_MainViewModel()
        {
            searchCommand = new RelayCommand(param => findMatchingMolds(), param => true);
        }

        /// <summary>
        /// Starts the algorithm to find matching molds based on the entered product.
        /// </summary>
        public void findMatchingMolds()
        {
            // Search by SAP-Nr.
            if (this.searchByProductId)
            {
                // Product Disc
                if (this.searchTypeProduct)
                {
                    try
                    {
                        using (var db = new GießformDBContext())
                        {
                            this.product = new Product();
                            product = db.ProductDiscs.Find(productId);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Suche fehlgeschlagen. Überprüfe die Db Verbindung!");
                    }
                }

                // Product Cup
                else
                {
                    try
                    {
                        using (var db = new GießformDBContext())
                        {
                            this.product = new Product();
                            product = db.ProductCups.Find(productId);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Suche fehlgeschlagen. Überprüfe die Db Verbindung!");
                    }
                }
                
            }

            // Search via product parameters
            else
            {
                // Product Disc
                if (this.searchTypeProduct)
                {
                    if (this.OuterDiameter == 0
                    || this.InnerDiameter == 0
                    || this.Height == 0
                    // || productdisc.FactorPU == null
                    // || productdisc.FactorPU == 0
                     || this.BTC == null)
                    {
                        MessageBox.Show("Bitte alle Werte ausfüllen!");
                    }
                    else
                    {
                        this.product = new Product();
                        this.productDisc = new ProductDisc();
                        this.productDisc.OuterDiameter = this.OuterDiameter;
                        this.productDisc.InnerDiameter = this.InnerDiameter;
                        this.productDisc.Height = this.Height;
                        this.productDisc.HcHoles = this.HcHoles != null ? this.HcHoles : null;
                        this.productDisc.HcHoleDiameter = this.HcHoleDiameter != null ? this.HcHoleDiameter : null;
                        this.productDisc.HcDiameter = this.HcDiameter != null ? this.HcDiameter : null;
                        this.productDisc.BTC = this.BTC != null ? this.BTC : null;
                        this.productDisc.FactorPU = this.FactorPU != null ? this.FactorPU : null;
                        this.product = productDisc;
                    }
                }

                // Product Cup
                else
                {
                    /*if ()
                    {
                        MessageBox.Show("Bitte alle Werte ausfüllen!");
                    }
                    else
                    {
                        product = productCup;
                        this.productCup = new ProductCup();
                    }*/
                }
            }

            // Create new ProgramLogic --> start algorithm to search for fitting molds
            if (product != null)
            {
                this.programLogic = new SearchJob(product);
                this.productSearchOutput.Clear();
                foreach (var compareObject in programLogic.finalOutput)
                {
                    this.productSearchOutput.Add(compareObject);
                }
            }
            else
            {
                MessageBox.Show("Kein Produkt bekannt!");
            }
        }
    }
}