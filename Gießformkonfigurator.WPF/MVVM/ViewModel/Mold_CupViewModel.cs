//-----------------------------------------------------------------------
// <copyright file="Mold_CupViewModel.cs" company="PlaceholderCompany">
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
    class Mold_CupViewModel
    {
        public SingleMoldCup singleMoldCup { get; set; }

        public ICommand insertIntoDbCmd { get; set; }

        public Mold_CupViewModel()
        {
            singleMoldCup = new SingleMoldCup();
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateData());
        }

        public void insertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.SingleMoldCups.Add(this.singleMoldCup);
                    db.SaveChanges();
                    MessageBox.Show("Gießform erfolgreich hinzugefügt.");
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
