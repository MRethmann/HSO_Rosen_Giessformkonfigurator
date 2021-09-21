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
    using Gießformkonfigurator.WPF.MVVM.Model.Logic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;

    class Search_MainViewModel : ObservableObject
    {
        public ObservableCollection<CompareObject> productSearchOutput { get; } = new ObservableCollection<CompareObject>();

        private ProgramLogic programLogic { get; set; }

        public Visibility IsLoading { get; set; } = Visibility.Hidden;

        public string listSize { get; set; }

        public int productId { get; set; }

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
                Hc1Holes = 12,
                Hc1Diameter = 337.59m,
                Hc1HoleDiameter = 14,
                Hc2Holes = 12,
                Hc2Diameter = 286.44m,
                Hc2HoleDiameter = 14,
                Hc3Holes = null,
                Hc3Diameter = null,
                Hc3HoleDiameter = null
            };

            //using (var db = new GießformDBContext())
            //{
            //    product = db.ProductDiscs.Find(productId);
            //}

            if (product != null)
            {
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