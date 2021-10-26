//-----------------------------------------------------------------------
// <copyright file="Search_MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Giessformkonfigurator.WPF.Core;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_components;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_products;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
    using Giessformkonfigurator.WPF.MVVM.Model.Logic;
    using log4net;

    /// <summary>
    /// Primary View which contains all elements and information used for the mold search.
    /// </summary>
    public class Search_MainViewModel : ObservableObject
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Search_MainViewModel));

        /// <summary>
        /// Initializes a new instance of the <see cref="Search_MainViewModel"/> class.
        /// </summary>
        public Search_MainViewModel()
        {
            this.SearchCommand = new RelayCommand(param => this.FindMatchingMolds(), param => this.ValidateInput());
        }

        /// <summary>
        /// Gets Mold Search Output ressource filled by SearchJob.
        /// </summary>
        public ObservableCollection<CompareObject> ProductSearchOutput { get; } = new ObservableCollection<CompareObject>();

        public Visibility IsLoading { get; set; } = Visibility.Hidden;

        /// <summary>
        /// Gets or Sets a value indicating whether Property determines the search method.
        /// True = Search via SAP number.
        /// False = Search by entering product parameters manually.
        /// </summary>
        public bool SearchByProductId { get; set; } = true;

        /// <summary>
        /// Gets or Sets a value indicating whether the Property that determines the product type in the GUI via Binding.
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

        /// <summary>
        /// Gets or Sets the possible PUFactors. Used as comboboxitems in GUI.
        /// </summary>
        // public List<decimal?> PUFactors { get; set; } = new List<decimal?>() { 1.017m, 1.0175m, 1.023m, 1.025m };

        /// <summary>
        /// Gets or Sets selected factorPu via dropdown in GUI.
        /// </summary>
        public decimal SelectedFactorPU { get; set; } = 1.023m;

        public string BTC { get; set; }

        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the color for the post processing value. Negative values are marked in red. Used by the Convert "PostProcessingColorSelector".
        /// </summary>
        public string PostProcessColor { get; set; } = "Black";

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
                            this.SetProductDiscPuFactor();
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
                            this.SetProductCupPuFactor();
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
                        this.SetProductDiscPuFactor();
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
                        this.productCup = new ProductCup();
                        this.SetProductCupDimensions();
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

        public void SetProductDiscPuFactor()
        {
            // TODO: Prüfen, welcher PU-Faktor nun genutzt werden soll. Gedanke mit Andreas war, dass je ein Faktor für Single- und einer für MultiMolds verwendet wird. Hier könnte jeweils der Mittelwert der beiden Faktoren genutzt werden.
            // Denkbar ist auch, dass der größere Faktor, ab einer bestimmten Größe der Gießform zum Tragen kommt.
            if (this.ProductDisc.FactorPU == 0
                || this.ProductDisc.FactorPU == null)
            {
                this.ProductDisc.FactorPU = 1.017m;
            }

            this.ProductDisc.MultiMoldFactorPU = this.SelectedFactorPU;
        }

        public void SetProductCupPuFactor()
        {
            if (this.ProductCup.FactorPU == 0
                || this.ProductCup.FactorPU == null)
            {
                this.ProductCup.FactorPU = 1.017m;
            }

            this.ProductCup.MultiMoldFactorPU = this.SelectedFactorPU;
        }
    }
}