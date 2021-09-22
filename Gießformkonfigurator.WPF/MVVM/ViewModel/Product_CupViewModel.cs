//-----------------------------------------------------------------------
// <copyright file="Product_CupViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Gießformkonfigurator.WPF.Core;
using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
using System;
using System.Windows;
using System.Windows.Input;

namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    class Product_CupViewModel : ObservableObject
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public string BaseCup { get; set; }

        public decimal InnerDiameter { get; set; }

        public decimal? FactorPU { get; set; }

        public string BTC { get; set; }

        public ICommand insertIntoDbCmd { get; set; }

        public Product_CupViewModel()
        {
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateData());
        }

        public void insertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    ProductCup productCup = new ProductCup()
                    {
                        ID = this.ID,
                        Description = this.Description,
                        BaseCup = this.BaseCup,
                        InnerDiameter = this.InnerDiameter,
                        FactorPU = this.FactorPU,
                        BTC = this.BTC
                    };
                    db.ProductCups.Add(productCup);
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
