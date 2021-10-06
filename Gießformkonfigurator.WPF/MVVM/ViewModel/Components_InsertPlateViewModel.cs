//-----------------------------------------------------------------------
// <copyright file="Components_InsertPlateViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    class Components_InsertPlateViewModel : ObservableObject
    {
        public InsertPlate insertPlate { get; set; }

        public ICommand insertIntoDbCmd { get; set; }

        public Components_InsertPlateViewModel()
        {
            this.insertPlate = new InsertPlate() { HasKonus = true } ;
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateInput());
        }

        public void insertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.InsertPlates.Add(this.insertPlate);
                    db.SaveChanges();
                    MessageBox.Show("Einlegeplatte erfolgreich hinzugefügt.");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e + "Fehler beim Hinzufügen.");
                }

            }
        }

        public bool validateInput()
        {
            if (insertPlate.ID.ToString().Length <= 1
                || insertPlate.OuterDiameter == 0
                || insertPlate.Height == 0
                || (insertPlate.HasKonus && (((insertPlate.InnerKonusMin ?? 0) == 0) || ((insertPlate.InnerKonusMax ?? 0) == 0) || ((insertPlate.InnerKonusAngle ?? 0) == 0) || ((insertPlate.KonusHeight ?? 0) == 0)))
                || (insertPlate.HasHoleguide && ((insertPlate.InnerDiameter ?? 0) == 0)))
            {
                return false;
            }

            return true;
        }
    }
}
