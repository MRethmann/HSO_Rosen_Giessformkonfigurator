//-----------------------------------------------------------------------
// <copyright file="Product_CupViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Gießformkonfigurator.WPF.Core;
using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    class Product_CupViewModel : ObservableObject
    {
        public List<Decimal?> PUFactors { get; set; } = new List<decimal?>() { 1.017m, 1.0175m, 1.023m, 1.025m };

        public ProductCup productCup { get; set; }

        public ICommand insertIntoDbCmd { get; set; }

        public Product_CupViewModel()
        {
            this.productCup = new ProductCup() { FactorPU = 1.017m };
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateInput());
        }

        public void insertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.ProductCups.Add(this.productCup);
                    db.SaveChanges();
                    MessageBox.Show("Produkt erfolgreich hinzugefügt.");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e + "Fehler beim Hinzufügen.");
                }

            }
        }

        public bool validateInput()
        {
            if (productCup.ID.ToString().Length <= 1
                || string.IsNullOrWhiteSpace(productCup.BaseCup)
                || productCup.InnerDiameter == 0)
            {
                return false;
            }

            return true;
        }

    }
}
