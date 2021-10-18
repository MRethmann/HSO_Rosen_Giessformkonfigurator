//-----------------------------------------------------------------------
// <copyright file="Components_RingViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.ViewModel
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using Giessformkonfigurator.WPF.Core;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_components;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_supportClasses;

    class Components_RingViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Components_RingViewModel"/> class.
        /// </summary>
        public Components_RingViewModel()
        {
            this.Ring = new Ring();
            this.InsertIntoDbCmd = new RelayCommand(param => this.InsertIntoDb(), param => this.ValidateInput());
        }

        public Ring Ring { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

        public void InsertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.Rings.Add(this.Ring);
                    db.SaveChanges();
                    MessageBox.Show("Ring erfolgreich hinzugefügt");
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
            if (this.Ring.ID.ToString().Length <= 1
                || this.Ring.OuterDiameter == 0
                || this.Ring.Height == 0
                || this.Ring.FillHeightMax == 0
                || this.Ring.InnerDiameter == 0
                || this.Ring.OuterDiameter < this.Ring.InnerDiameter
                || (this.Ring.HasKonus && (((this.Ring.InnerKonusMin ?? 0) == 0) || ((this.Ring.InnerKonusMax ?? 0) == 0) || ((this.Ring.InnerKonusAngle ?? 0) == 0) || ((this.Ring.KonusHeight ?? 0) == 0))))
            {
                return false;
            }

            return true;
        }
    }
}
