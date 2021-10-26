//-----------------------------------------------------------------------
// <copyright file="Components_BaseplateViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using Giessformkonfigurator.WPF.Core;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_components;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_supportClasses;

    class Components_BaseplateViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Components_BaseplateViewModel"/> class.
        /// </summary>
        public Components_BaseplateViewModel()
        {
            this.Baseplate = new Baseplate() { HasKonus = true };
            this.InsertIntoDbCmd = new RelayCommand(param => this.InsertIntoDb(), param => this.ValidateInput());
        }

        public Baseplate Baseplate { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

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
        /// Impede wrong user input which may result in wrong mold search output. Activates Button if true.
        /// </summary>
        /// <returns>True (active Button) if all input data is valid.</returns>
        public bool ValidateInput()
        {
            if (this.Baseplate.ID.ToString().Length <= 1
                || this.Baseplate.OuterDiameter == 0
                || this.Baseplate.Height == 0
                || this.Baseplate.OuterDiameter < this.Baseplate.InnerDiameter
                || this.Baseplate.OuterKonusMax == 0
                || this.Baseplate.OuterKonusMin == 0
                || this.Baseplate.OuterKonusAngle == 0
                || this.Baseplate.KonusHeight == 0
                || (this.Baseplate.HasKonus && (((this.Baseplate.InnerKonusMin ?? 0) == 0) || ((this.Baseplate.InnerKonusMax ?? 0) == 0) || ((this.Baseplate.InnerKonusAngle ?? 0) == 0)))
                || (this.Baseplate.HasHoleguide && ((this.Baseplate.InnerDiameter ?? 0) == 0)))
            {
                return false;
            }

            return true;
        }
    }
}