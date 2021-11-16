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

        // public decimal? HcDiameter { get; set; }

        // public int? HcHoles { get; set; }

        // public decimal? HcHoleDiameter { get; set; }

        public string BTC { get; set; }

        public int ProductId { get; set; }

        public string CupType { get; set; }

        public decimal CupSize { get; set; }

        public List<string> CupFormTypes { get; set; } = new List<string>() { "U", "L", "UH", "TL", "H", "L Radaufbau", "DD", "M" };

        public ICommand SearchCommand { get; set; }

        private SearchJob SearchJob { get; set; }

        /// <summary>
        /// Starts the algorithm to find matching molds based on the entered product.
        /// </summary>
        public void FindMatchingMolds()
        {
            this.SearchJob = null;
            this.ProductDisc = null;
            this.ProductCup = null;

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
                            this.ProductDisc = db.ProductDiscs.Find(this.ProductId);

                            if (this.ProductDisc != null)
                            {
                                this.SetProductDiscPuFactor();
                                this.SearchJob = new SearchJob(this.ProductDisc);
                                Log.Info($"ProductDisc search via SAP-Nr. started for product: {this.ProductDisc}");
                            }
                            else
                            {
                                MessageBox.Show($"Das Produkt mit der SAP-Nr. {this.ProductId} konnte nicht gefunden werden.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Es ist ein Problem aufgetreten. Bitte wenden Sie sich an den Systemadministrator." + Environment.NewLine + Environment.NewLine + ex);
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
                            this.ProductCup = db.ProductCups.Find(this.ProductId);

                            if (this.ProductCup != null)
                            {
                                this.SetProductCupPuFactor();
                                this.SearchJob = new SearchJob(this.ProductCup);
                                Log.Info($"ProductCup search via SAP-Nr. started for product: {this.ProductCup}");
                            }
                            else
                            {
                                MessageBox.Show($"Das Produkt mit der SAP-Nr. {this.ProductId} konnte nicht gefunden werden.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Es ist ein Problem aufgetreten. Bitte wenden Sie sich an den Systemadministrator." + Environment.NewLine + Environment.NewLine + ex);
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
                        MessageBox.Show("Die eingegebenen Werte sind unvollständig oder fehlerhaft.");
                    }
                    else
                    {
                        this.ProductDisc = new ProductDisc();
                        this.ProductDisc.OuterDiameter = this.OuterDiameter;
                        this.ProductDisc.InnerDiameter = this.InnerDiameter;
                        this.ProductDisc.Height = this.Height;
                        // this.ProductDisc.HcHoles = this.HcHoles != null ? this.HcHoles : null;
                        // this.ProductDisc.HcHoleDiameter = this.HcHoleDiameter != null ? this.HcHoleDiameter : null;
                        // this.ProductDisc.HcDiameter = this.HcDiameter != null ? this.HcDiameter : null;
                        this.ProductDisc.BTC = this.BTC != null ? this.BTC : null;
                        this.SetProductDiscPuFactor();
                        this.SearchJob = new SearchJob(this.ProductDisc);
                        Log.Info($"ProductDisc search started via manual entry for product: {this.ProductDisc.OuterDiameter}, {this.ProductDisc.InnerDiameter}, {this.ProductDisc.Height}, {this.ProductDisc.BTC} - (OD, ID, T, BTC)");
                    }
                }

                // Product Cup
                else
                {
                    if (this.CupType == null
                        || this.CupFormTypes.Contains($"{this.CupType}") == false
                        || this.CupSize == 0
                        || this.InnerDiameter == 0)
                    {
                        this.ProductCup = null;
                        MessageBox.Show("Die eingegebenen Werte sind unvollständig oder fehlerhaft.");
                    }
                    else
                    {
                        this.ProductCup = new ProductCup();
                        this.ProductCup.InnerDiameter = this.InnerDiameter;
                        this.ProductCup.CupType = this.CupType;
                        this.ProductCup.Size = this.CupSize;
                        this.ProductCup.BTC = this.BTC != null ? this.BTC : null;
                        this.SetProductCupPuFactor();
                        this.SearchJob = new SearchJob(this.ProductCup);
                        Log.Info($"ProductDisc search started via manual entry for product: {this.ProductCup.Size}, {this.ProductCup.CupType}, {this.ProductCup.InnerDiameter}, {this.ProductCup.BTC} - (Size, CupType, ID, BTC)");
                    }
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
                || (this.SearchByProductId == false && this.SearchTypeProduct == true && (this.OuterDiameter == 0 || this.InnerDiameter == 0 || this.Height == 0))
                || ((this.SearchByProductId == false && this.SearchTypeProduct == false) && (this.CupSize == 0 || this.CupType == null || this.InnerDiameter == 0)))
            {
                return false;
            }

            return true;
        }

        public void SetProductDiscPuFactor()
        {
            this.ProductDisc.FactorPU = 1.01725m;
            this.ProductDisc.MultiMoldFactorPU = 1.024m;
        }

        public void SetProductCupPuFactor()
        {
            this.ProductCup.FactorPU = 1.01725m;
            this.ProductCup.MultiMoldFactorPU = 1.024m;
        }
    }
}