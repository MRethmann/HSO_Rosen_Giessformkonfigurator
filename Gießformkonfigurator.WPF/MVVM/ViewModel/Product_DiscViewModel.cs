//-----------------------------------------------------------------------
// <copyright file="Product_DiscViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;

    class Product_DiscViewModel : ObservableObject
    {
        public Product_DiscViewModel()
        {
            this.ProductDisc = new ProductDisc() { FactorPU = 1.017m };
            this.InsertIntoDbCmd = new RelayCommand(param => this.InsertIntoDb(), param => this.ValidateInput());
        }

        public List<decimal?> PUFactors { get; set; } = new List<decimal?>() { 1.017m, 1.0175m, 1.023m, 1.025m };

        public ProductDisc ProductDisc { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

        public void InsertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.ProductDiscs.Add(this.ProductDisc);
                    db.SaveChanges();
                    MessageBox.Show("Produkt erfolgreich hinzugefügt");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e + "Fehler beim Hinzufügen.");
                }
            }
        }

        public bool ValidateInput()
        {
            if (this.ProductDisc.ID.ToString().Length <= 1
                || this.ProductDisc.OuterDiameter == 0
                || this.ProductDisc.InnerDiameter == 0
                || this.ProductDisc.Height == 0
                || this.ProductDisc.OuterDiameter < this.ProductDisc.InnerDiameter)
            {
                return false;
            }

            return true;
        }
    }
}
