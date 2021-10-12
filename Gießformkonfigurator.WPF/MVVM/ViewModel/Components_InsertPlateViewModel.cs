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
        public InsertPlate InsertPlate { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

        public Components_InsertPlateViewModel()
        {
            this.InsertPlate = new InsertPlate() { HasKonus = true } ;
            InsertIntoDbCmd = new RelayCommand(param => InsertIntoDb(), param => ValidateInput());
        }

        public void InsertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.InsertPlates.Add(this.InsertPlate);
                    db.SaveChanges();
                    MessageBox.Show("Einlegeplatte erfolgreich hinzugefügt");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e + "Fehler beim Hinzufügen");
                }

            }
        }

        public bool ValidateInput()
        {
            if (InsertPlate.ID.ToString().Length <= 1
                || InsertPlate.OuterDiameter == 0
                || InsertPlate.Height == 0
                || InsertPlate.OuterDiameter < InsertPlate.InnerDiameter
                || (InsertPlate.HasKonus && (((InsertPlate.InnerKonusMin ?? 0) == 0) || ((InsertPlate.InnerKonusMax ?? 0) == 0) || ((InsertPlate.InnerKonusAngle ?? 0) == 0) || ((InsertPlate.KonusHeight ?? 0) == 0)))
                || (InsertPlate.HasHoleguide && ((InsertPlate.InnerDiameter ?? 0) == 0)))
            {
                return false;
            }

            return true;
        }
    }
}
