//-----------------------------------------------------------------------
// <copyright file="Components_BaseplateViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;

    class Components_BaseplateViewModel : ObservableObject
    {
        public Baseplate Baseplate { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

        public Components_BaseplateViewModel()
        {
            Baseplate = new Baseplate() { HasKonus = true };
            InsertIntoDbCmd = new RelayCommand(param => InsertIntoDb(), param => ValidateInput());
        }

        // TODO: Implement some kind of validation that prevents wrong input.
        public void InsertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.Baseplates.Add(this.Baseplate);
                    db.SaveChanges();
                    MessageBox.Show("Grundplatte erfolgreich hinzugefügt");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e + "Fehler beim Hinzufügen");
                }

            }
        }

        /// <summary>
        /// Validates if all required fields are filled out and activates the button if true.
        /// </summary>
        /// <returns>True if all required fields are filled.</returns>
        public bool ValidateInput()
        {
            if (Baseplate.ID.ToString().Length <= 1
                || Baseplate.OuterDiameter == 0
                || Baseplate.Height == 0
                || Baseplate.OuterDiameter < Baseplate.InnerDiameter
                || ((Baseplate.OuterKonusMax ?? 0) == 0)
                || ((Baseplate.OuterKonusMin ?? 0) == 0)
                || ((Baseplate.OuterKonusAngle ?? 0) == 0)
                || ((Baseplate.KonusHeight ?? 0) == 0)
                || (Baseplate.HasKonus && (((Baseplate.InnerKonusMin ?? 0) == 0) || ((Baseplate.InnerKonusMax ?? 0) == 0) || ((Baseplate.InnerKonusAngle ?? 0) == 0)))
                || (Baseplate.HasHoleguide && ((Baseplate.InnerDiameter ?? 0) == 0)))
            {
                return false;
            }
            return true;
        }
    }
}
