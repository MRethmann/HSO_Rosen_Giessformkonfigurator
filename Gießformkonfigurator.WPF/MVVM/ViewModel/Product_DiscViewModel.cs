//-----------------------------------------------------------------------
// <copyright file="Product_DiscViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    class Product_DiscViewModel : ObservableObject, IDataErrorInfo
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public decimal OuterDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal InnerDiameter { get; set; }

        public decimal? Hc1Diameter { get; set; }

        public decimal? Hc1Holes { get; set; }

        public decimal? Hc1HoleDiameter { get; set; }

        public decimal? Hc2Diameter { get; set; }

        public decimal? Hc2Holes { get; set; }

        public decimal? Hc2HoleDiameter { get; set; }

        public decimal? Hc3Diameter { get; set; }

        public decimal? Hc3Holes { get; set; }

        public decimal? Hc3HoleDiameter { get; set; }

        public ICommand insertIntoDbCmd { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;

                switch (columnName)
                {
                    case nameof(ID):
                        if (this.ID == 0)
                            error = "SAP-Nr. muss ausgefüllt werden.";
                        break;
                }

                return error;
            }
        }
        public string Error => string.Empty;

        public Product_DiscViewModel()
        {
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateData());
        }

        public void insertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    ProductDisc productDisc = new ProductDisc()
                    {
                        ID = this.ID,
                        OuterDiameter = this.OuterDiameter,
                        Height = this.Height,
                        InnerDiameter = this.InnerDiameter,
                        Hc1Holes = this.Hc1Holes,
                        Hc1Diameter = this.Hc1Diameter,
                        Hc1HoleDiameter = this.Hc1HoleDiameter,
                        Hc2Holes = this.Hc2Holes,
                        Hc2Diameter = this.Hc2Diameter,
                        Hc2HoleDiameter = this.Hc2HoleDiameter,
                        Hc3Holes = this.Hc3Holes,
                        Hc3Diameter = this.Hc3Diameter,
                        Hc3HoleDiameter = this.Hc3HoleDiameter
                    };
                    db.ProductDiscs.Add(productDisc);
                    db.SaveChanges();
                    MessageBox.Show("Produkt erfolgreich hinzugefügt.");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e + "Fehler beim Hinzufügen.");
                }

            }
        }
        private bool validateData()
        {
            return true;
        }
    }
}
