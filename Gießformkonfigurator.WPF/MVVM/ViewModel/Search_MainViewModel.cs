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
                ID = 78700,
                Description = "Testprodukt",
                OuterDiameter = 328,
                InnerDiameter = 82,
                Height = 15,
                HcHoles = null,
                HcDiameter = null,
                HcHoleDiameter = null,
                FactorPU = 1.0175m, 
                BTC = null,
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
                        //((ModularMold)compareObject.Mold).ListOuterRings.Add(new Model.Db_components.Ring { Description = "ALBERTOOO" , ID = 123151, OuterDiameter = 5, ToleranceInnerDiameter = "5", InnerDiameter = 5, ToleranceOuterDiameter = "5", FillHeightMax = 12, HasKonus = true, Height = 55});
                        this.productSearchOutput.Add(compareObject);
                        //compareObject.postProcessing.Add("Test1");
                        //compareObject.postProcessing.Add("Test2");
                    }
                    /*var test = new CompareObject(product, new ModularMold(new Baseplate(), new Ring(), new InsertPlate(), new Core())) { finalRating = 80 };
                    ((ModularMold)test.Mold).ListCoreRings.Add(new Tuple<Ring, Ring, decimal?>(new Ring() { ID = 1 }, new Ring() ,0.1m));
                    test.bolts.Add(new Tuple<Bolt, decimal?>(new Bolt() { Description = "BoltTest", ID = 123 }, 0.15m));
                    test.bolts.Add(new Tuple<Bolt, decimal?>(new Bolt() { Description = "BoltTest1", ID = 23 }, 0.15m));
                    test.bolts.Add(new Tuple<Bolt, decimal?>(new Bolt() { Description = "BoltTest2", ID = 2331 }, 0.15m));
                    test.bolts.Add(new Tuple<Bolt, decimal?>(new Bolt() { Description = "BoltTest3", ID = 12 }, 0.15m));
                    test.bolts.Add(new Tuple<Bolt, decimal?>(new Bolt() { Description = "BoltTest4", ID = 541 }, 0.15m));
                    test.bolts.Add(new Tuple<Bolt, decimal?>(new Bolt() { Description = "BoltTest4", ID = 541 }, 0.15m));
                    test.bolts.Add(new Tuple<Bolt, decimal?>(new Bolt() { Description = "BoltTest4", ID = 541 }, 0.15m));
                    test.bolts.Add(new Tuple<Bolt, decimal?>(new Bolt() { Description = "BoltTest4", ID = 541 }, 0.15m));
                    test.bolts.Add(new Tuple<Bolt, decimal?>(new Bolt() { Description = "BoltTest4", ID = 541 }, 0.15m));
                    test.bolts.Add(new Tuple<Bolt, decimal?>(new Bolt() { Description = "BoltTest4", ID = 541 }, 0.15m));
                    this.productSearchOutput.Add(test);
                    this.productSearchOutput.Add(new CompareObject(product, new ModularMold(new Cupform(), new Core(), new InsertPlate())) { finalRating = 90 });
                    this.productSearchOutput.Add(new CompareObject(product, new SingleMoldDisc()) { finalRating = 55 });
                    this.productSearchOutput.Add(new CompareObject(product, new SingleMoldCup()) { finalRating = 85 });*/

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