//-----------------------------------------------------------------------
// <copyright file="Search_MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
    using Gießformkonfigurator.WPF.MVVM.Model.Logic;
    using log4net;

    /// <summary>
    /// Primary View which contains all elements and information used for the mold search.
    /// </summary>
    public class Search_MainViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Search_MainViewModel"/> class.
        /// </summary>
        public Search_MainViewModel()
        {
            this.SearchCommand = new RelayCommand(param => this.FindMatchingMolds(), param => this.ValidateInput());
        }

        private static readonly ILog Log = LogManager.GetLogger(typeof(Search_MainViewModel));

        /// <summary>
        /// Mold Search Output ressource filled by SearchJob.
        /// </summary>
        public ObservableCollection<CompareObject> ProductSearchOutput { get; } = new ObservableCollection<CompareObject>();

        public Visibility IsLoading { get; set; } = Visibility.Hidden;

        /// <summary>
        /// Property determines the search method. 
        /// True = Search via SAP number. 
        /// False = Search by entering product parameters manually.
        /// </summary>
        public bool SearchByProductId { get; set; } = true;

        /// <summary>
        /// Gets or Sets the Property that determines the product type in the GUI via Binding.
        /// True = ProductDisc.
        /// False = ProductCup.
        /// </summary>
        public bool SearchTypeProduct { get; set; } = true;

        public ProductDisc ProductDisc { get; set; }

        public ProductCup ProductCup { get; set; }

        public decimal OuterDiameter { get; set; }

        public decimal InnerDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal? HcDiameter { get; set; }

        public int? HcHoles { get; set; }

        public decimal? HcHoleDiameter { get; set; }

        public List<decimal?> PUFactors { get; set; } = new List<decimal?>() { 1.017m, 1.0175m, 1.023m, 1.025m };

        public decimal? SelectedFactorPU { get; set; } = 1.017m;

        public string BTC { get; set; }

        public int ProductId { get; set; }

        public ICommand SearchCommand { get; set; }

        private SearchJob SearchJob { get; set; }

        /// <summary>
        /// Starts the algorithm to find matching molds based on the entered product.
        /// </summary>
        public void FindMatchingMolds()
        {
            // Search by SAP-Nr.
            if (this.SearchByProductId)
            {
                // Product Disc
                if (this.SearchTypeProduct)
                {
                    try
                    {
                        using (var db = new GießformDBContext())
                        {
                            this.ProductDisc = new ProductDisc();
                            this.ProductDisc = db.ProductDiscs.Find(this.ProductId);
                            this.SearchJob = new SearchJob(this.ProductDisc);
                            Log.Info($"ProductDisc search via SAP-Nr. started for product: {this.ProductDisc}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Suche fehlgeschlagen. Überprüfe die Db Verbindung!" + ex);
                        Log.Error(ex);
                    }
                }

                // Product Cup
                else
                {
                    try
                    {
                        using (var db = new GießformDBContext())
                        {
                            this.ProductCup = new ProductCup();
                            this.ProductCup = db.ProductCups.Find(this.ProductId);
                            this.SearchJob = new SearchJob(this.ProductCup);
                            Log.Info($"ProductCup search via SAP-Nr. started for product: {this.ProductCup}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Suche fehlgeschlagen. Überprüfe die Db Verbindung!");
                        Log.Error(ex);
                    }
                }
            }

            // Search via product parameters
            else
            {
                // Product Disc
                if (this.SearchTypeProduct)
                {
                    if (this.OuterDiameter == 0
                    || this.InnerDiameter == 0
                    || this.Height == 0
                    || this.OuterDiameter < this.InnerDiameter)
                    {
                        this.ProductDisc = null;
                        MessageBox.Show("Bitte überprüfe die eingegebenen Werte!");
                    }
                    else
                    {
                        this.ProductDisc = new ProductDisc();
                        this.ProductDisc.OuterDiameter = this.OuterDiameter;
                        this.ProductDisc.InnerDiameter = this.InnerDiameter;
                        this.ProductDisc.Height = this.Height;
                        this.ProductDisc.HcHoles = this.HcHoles != null ? this.HcHoles : null;
                        this.ProductDisc.HcHoleDiameter = this.HcHoleDiameter != null ? this.HcHoleDiameter : null;
                        this.ProductDisc.HcDiameter = this.HcDiameter != null ? this.HcDiameter : null;
                        this.ProductDisc.BTC = this.BTC != null ? this.BTC : null;
                        this.ProductDisc.FactorPU = this.SelectedFactorPU;
                        this.SearchJob = new SearchJob(this.ProductDisc);
                        Log.Info($"ProductDisc search started via manual entry for product: {this.ProductDisc.OuterDiameter}, {this.ProductDisc.InnerDiameter}, {this.ProductDisc.Height}, {this.ProductDisc.BTC}, {this.ProductDisc.FactorPU} - (OD, ID, T, BTC, Factor)");
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

            this.ProductSearchOutput.Clear();
            if (this.SearchJob != null)
            {
                foreach (var compareObject in this.SearchJob.FinalOutput)
                {
                    this.ProductSearchOutput.Add(compareObject);
                }
            }
        }

        public bool ValidateInput()
        {
            if ((this.SearchByProductId == true && this.ProductId == 0)
                || (this.SearchByProductId == false && (this.OuterDiameter == 0 || this.InnerDiameter == 0 || this.Height == 0)))
            {
                return false;
            }

            return true;
        }
    }
}