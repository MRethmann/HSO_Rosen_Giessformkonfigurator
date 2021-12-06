//-----------------------------------------------------------------------
// <copyright file="Components_CupformViewModel.cs" company="PlaceholderCompany">
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

    class Components_CupformViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Components_CupformViewModel"/> class.
        /// </summary>
        public Components_CupformViewModel()
        {
            this.Cupform = new Cupform() { HasCore = true };
            this.InsertIntoDbCmd = new RelayCommand(param => this.InsertIntoDb(), param => this.ValidateInput());
        }

        public Cupform Cupform { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

        public void InsertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.Cupforms.Add(this.Cupform);
                    db.SaveChanges();
                    MessageBox.Show("Cupform erfolgreich hinzugefügt");
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
            if (this.Cupform.ID.ToString().Length <= 1
                || this.Cupform.Size == 0
                || this.Cupform.InnerDiameter == 0
                || this.Cupform.CupType == null
                || (this.Cupform.HasKonus && (((this.Cupform.InnerKonusMin ?? 0) == 0) || ((this.Cupform.InnerKonusMax ?? 0) == 0) || ((this.Cupform.InnerKonusAngle ?? 0) == 0) || ((this.Cupform.InnerKonusHeight ?? 0) == 0))))
            {
                return false;
            }

            return true;
        }
    }
}
