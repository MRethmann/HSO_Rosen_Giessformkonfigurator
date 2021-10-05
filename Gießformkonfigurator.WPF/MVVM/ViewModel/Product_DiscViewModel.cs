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

    class Product_DiscViewModel : ObservableObject
    {
        public ProductDisc productDisc { get; set; }

        public ICommand insertIntoDbCmd { get; set; }

        public Product_DiscViewModel()
        {
            this.productDisc = new ProductDisc();
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => true);
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
    }
}
