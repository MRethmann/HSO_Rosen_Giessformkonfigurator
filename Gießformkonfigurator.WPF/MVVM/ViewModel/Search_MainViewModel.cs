//-----------------------------------------------------------------------
// <copyright file="Search_MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
    using Gießformkonfigurator.WPF.MVVM.Model.Logic;
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Input;

    class Search_MainViewModel : ObservableObject
    {
        public ObservableCollection<CompareObject> productSearchOutput { get; } = new ObservableCollection<CompareObject>();

        private ProgramLogic programLogic { get; set; }

        public Visibility IsLoading { get; set; } = Visibility.Hidden;

        public string listSize { get; set; }

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
            //Product product = new ProductDisc() { OuterDiameter = 310.00m, InnerDiameter = 42.00m, Height = 30.00m, Hc1Holes = 8, Hc1Diameter = 10, Hc1HoleDiameter = 1, Hc2Holes = 8, Hc2Diameter = 20, Hc2HoleDiameter = 2, Hc3Holes = 8, Hc3Diameter = 30, Hc3HoleDiameter = 3 };
            
            Product product = new ProductDisc()
            {
                ID = 000,
                OuterDiameter = 310.00m,
                InnerDiameter = 275.00m,
                Height = 20.00m,
                HcHoles = 12,
                HcDiameter = 337.59m,
                HcHoleDiameter = 14,
                FactorPU = 1.0175m
            };

            /*using (var db = new GießformDBContext())
            {
                product = db.ProductDiscs.Find(productId); 
            }*/

            if (product != null)
            {
                // Validate if factorPU is filled in GUI or set in database.
                // ToDo: Make it possible so the user can choose which Value to take.
                if (product.FactorPU != null && this.FactorPU != null && product.FactorPU != this.FactorPU)
                {
                    MessageBox.Show("Faktor-PU Eingabe stimmt nicht mit dem Produkteintrag in der Datenbank überein. In der Datenbank ist folgender Faktor hinterlegt: " + product.FactorPU);
                }
                else if (product.FactorPU == null && this.FactorPU == null)
                {
                    MessageBox.Show("Bitte fülle den Faktor-PU aus!");
                }
                else
                {
                    if (product.FactorPU == null)
                    {
                        product.FactorPU = Convert.ToDecimal(this.FactorPU);
                    }

                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                    this.IsLoading = Visibility.Visible;

                    programLogic = new ProgramLogic(product);

                    this.productSearchOutput.Clear();

                    foreach (var compareObject in programLogic.finalOutput)
                    {
                        this.productSearchOutput.Add(compareObject);
                    }

                    this.IsLoading = Visibility.Hidden;
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
                }
            }
            else
                MessageBox.Show("Es stimmt kein Produkt mit der eingegebenen SAP-Nr. überein. Bitte validiere die Eingabe!");
        }

        public bool validateSearch()
        {
            if (productId != 0)
                return true;
            else
                return false;
        }
    }
}