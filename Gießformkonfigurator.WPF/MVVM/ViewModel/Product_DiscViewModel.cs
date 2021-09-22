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

        public decimal? FactorPU { get; set; }

        public string BTC { get; set; }

        public decimal? HcDiameter { get; set; }

        public int? HcHoles { get; set; }

        public decimal? HcHoleDiameter { get; set; }


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
                        Description = this.Description,
                        OuterDiameter = this.OuterDiameter,
                        Height = this.Height,
                        InnerDiameter = this.InnerDiameter,
                        FactorPU = this.FactorPU,
                        BTC = this.BTC,
                        HcHoles = this.HcHoles,
                        HcDiameter = this.HcDiameter,
                        HcHoleDiameter = this.HcHoleDiameter,
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
