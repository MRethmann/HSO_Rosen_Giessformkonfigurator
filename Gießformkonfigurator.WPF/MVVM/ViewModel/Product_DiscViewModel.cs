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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    class Product_DiscViewModel : ObservableObject
    {
        public List<Decimal?> PUFactors { get; set; } = new List<decimal?>() { 1.017m, 1.0175m, 1.023m, 1.025m };

        public ProductDisc productDisc { get; set; }

        public ICommand insertIntoDbCmd { get; set; }

        public Product_DiscViewModel()
        {
            this.productDisc = new ProductDisc() { FactorPU = 1.017m };
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateInput());
        }

        public void insertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.ProductDiscs.Add(this.productDisc);
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
            if (productDisc.ID.ToString().Length <= 1
                || productDisc.OuterDiameter == 0
                || productDisc.InnerDiameter == 0
                || productDisc.Height == 0)
            {
                return false;
            }

            return true;
        }
    }
}
