//-----------------------------------------------------------------------
// <copyright file="Components_InsertPlateViewModel.cs" company="PlaceholderCompany">
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

    class Components_InsertPlateViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Components_InsertPlateViewModel"/> class.
        /// </summary>
        public Components_InsertPlateViewModel()
        {
            this.InsertPlate = new InsertPlate() { HasKonus = true };
            this.InsertIntoDbCmd = new RelayCommand(param => this.InsertIntoDb(), param => this.ValidateInput());
        }

        public InsertPlate InsertPlate { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

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

        /// <summary>
        /// Impede wrong user input which may result in wrong mold search output. Activates Button if true.
        /// </summary>
        /// <returns>True (active Button) if all input data is valid.</returns>
        public bool ValidateInput()
        {
            if (this.InsertPlate.ID.ToString().Length <= 1
                || this.InsertPlate.OuterDiameter == 0
                || this.InsertPlate.Height == 0
                || this.InsertPlate.OuterDiameter < this.InsertPlate.InnerDiameter
                || (this.InsertPlate.HasKonus && (((this.InsertPlate.InnerKonusMin ?? 0) == 0) || ((this.InsertPlate.InnerKonusMax ?? 0) == 0) || ((this.InsertPlate.InnerKonusAngle ?? 0) == 0) || ((this.InsertPlate.KonusHeight ?? 0) == 0)))
                || (this.InsertPlate.HasHoleguide && ((this.InsertPlate.InnerDiameter ?? 0) == 0)))
            {
                return false;
            }

            return true;
        }
    }
}
