//-----------------------------------------------------------------------
// <copyright file="Product_CupViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Input;
    using Giessformkonfigurator.WPF.Core;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_products;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_supportClasses;

    class Product_CupViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product_CupViewModel"/> class.
        /// </summary>
        public Product_CupViewModel()
        {
            this.ProductCup = new ProductCup() { FactorPU = 1.017m };
            this.InsertIntoDbCmd = new RelayCommand(param => this.InsertIntoDb(), param => this.ValidateInput());
        }

        public List<decimal?> PUFactors { get; set; } = new List<decimal?>() { 1.017m, 1.0175m, 1.023m, 1.025m };

        public ProductCup ProductCup { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

        public void InsertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.ProductCups.Add(this.ProductCup);
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
            if (this.ProductCup.ID.ToString().Length <= 1
                || string.IsNullOrWhiteSpace(this.ProductCup.BaseCup)
                || this.ProductCup.InnerDiameter == 0)
            {
                return false;
            }

            return true;
        }

    }
}
