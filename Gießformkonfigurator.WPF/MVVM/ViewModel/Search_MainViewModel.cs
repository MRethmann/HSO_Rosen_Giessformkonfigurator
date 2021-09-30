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

        /// <summary>
        /// Property used for the creating of a temporary product Disc via GUI parameter input.
        /// </summary>
        public ProductDisc productDisc { get; set; } = new ProductDisc();

        /// <summary>
        /// Property used for the creating of a temporary product Cup via GUI parameter input.
        /// </summary>
        public ProductCup productCup { get; set; } = new ProductCup();

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
            product = new Product();

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
                    if (this.productDisc.OuterDiameter == 0
                    || this.productDisc.InnerDiameter == 0
                    || this.productDisc.Height == 0)
                    // || productdisc.FactorPU == null
                    // || productdisc.FactorPU == 0
                    // || this.productDisc.BTC == null)
                    {
                        MessageBox.Show("Bitte alle Werte ausfüllen!");
                    }
                    else
                    {
                        product = productDisc;
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
                    }*/
                }
            }

            // Create new ProgramLogic --> start algorithm to search for fitting molds
            if (product != null)
            {
                programLogic = new ProgramLogic(product);
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