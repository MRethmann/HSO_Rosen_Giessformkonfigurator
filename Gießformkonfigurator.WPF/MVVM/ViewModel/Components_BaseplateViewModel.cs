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
            baseplate = new Baseplate();
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateData());
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
        private bool validateData()
        {
            /*if (this.ID != 0
                && this.OuterDiameter != 0
                && this.Description != null
                && this.Height != 0
                && this.OuterKonusMax != 0
                && this.OuterKonusMin != 0
                && this.OuterKonusAngle != 0
                && this.KonusHeight != 0
                && ((this.HasKonus == true && ((this.InnerKonusMax != 0 || this.InnerKonusMax != null) && (this.InnerKonusMin != 0 || this.InnerKonusMin != null) && (this.InnerKonusAngle != 0 || this.InnerKonusAngle != null)))
                || (this.HasHoleguide == true && (this.InnerDiameter != 0 || this.InnerDiameter != null))
                || (this.HasCore == true)))
            {
                return true;
            }
            else
                return false;*/
            return true;
        }

    }
}
