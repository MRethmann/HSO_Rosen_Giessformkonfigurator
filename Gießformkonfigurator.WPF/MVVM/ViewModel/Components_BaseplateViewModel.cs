//-----------------------------------------------------------------------
// <copyright file="Components_BaseplateViewModel.cs" company="PlaceholderCompany">
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

    class Components_BaseplateViewModel : ObservableObject
    {
        public Baseplate baseplate { get; set; }

        public ICommand insertIntoDbCmd { get; set; }

        public Components_BaseplateViewModel()
        {
            baseplate = new Baseplate() { HasKonus = true };
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateInput());
        }

        // TODO: Implement some kind of validation that prevents wrong input.
        public void insertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.Baseplates.Add(this.baseplate);
                    db.SaveChanges();
                    MessageBox.Show("Grundplatte erfolgreich hinzugefügt.");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e + "Fehler beim Hinzufügen.");
                }

            }
        }

        /// <summary>
        /// Validates if all required fields are filled out and activates the button if true.
        /// </summary>
        /// <returns>True if all required fields are filled.</returns>
        public bool validateInput()
        {
            if (baseplate.ID.ToString().Length <= 1
                || baseplate.OuterDiameter == 0
                || baseplate.Height == 0
                || ((baseplate.OuterKonusMax ?? 0) == 0)
                || ((baseplate.OuterKonusMin ?? 0) == 0)
                || ((baseplate.OuterKonusAngle ?? 0) == 0)
                || ((baseplate.KonusHeight ?? 0) == 0)
                || (baseplate.HasKonus && (((baseplate.InnerKonusMin ?? 0) == 0) || ((baseplate.InnerKonusMax ?? 0) == 0) || ((baseplate.InnerKonusAngle ?? 0) == 0)))
                || (baseplate.HasHoleguide && ((baseplate.InnerDiameter ?? 0) == 0)))
            {
                return false;
            }

            return true;
        }
    }
}
