//-----------------------------------------------------------------------
// <copyright file="Mold_DiscViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Gießformkonfigurator.WPF.Core;
using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;
using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
using System;
using System.Windows;
using System.Windows.Input;

namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    class Mold_DiscViewModel
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public decimal OuterDiameter { get; set; }

        public decimal InnerDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal? HcDiameter { get; set; }

        public int? HcHoles { get; set; }

        public decimal? BoltDiameter { get; set; }

        public ICommand insertIntoDbCmd { get; set; }

        public Mold_DiscViewModel()
        {
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateData());
        }

        public void insertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    SingleMoldDisc singleMoldDisc = new SingleMoldDisc()
                    {
                        ID = this.ID,
                        Description = this.Description,
                        OuterDiameter = this.OuterDiameter,
                        InnerDiameter = this.InnerDiameter,
                        Height = this.Height,
                        HcDiameter = this.HcDiameter,
                        HcHoles = this.HcHoles,
                        BoltDiameter = this.BoltDiameter
                    };
                    db.SingleMoldDiscs.Add(singleMoldDisc);
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
