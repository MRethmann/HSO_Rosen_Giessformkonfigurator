//-----------------------------------------------------------------------
// <copyright file="Components_CoreViewModel.cs" company="PlaceholderCompany">
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

    class Components_CoreViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Components_CoreViewModel"/> class.
        /// </summary>
        public Components_CoreViewModel()
        {
            this.Core = new Core() { HasKonus = true };
            this.InsertIntoDbCmd = new RelayCommand(param => this.InsertIntoDb(), param => this.ValidateInput());
        }

        public Core Core { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

        public void InsertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.Cores.Add(this.Core);
                    db.SaveChanges();
                    MessageBox.Show("Kern erfolgreich hinzugefügt");
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
            if (this.Core.ID.ToString().Length <= 1
                || this.Core.OuterDiameter == 0
                || this.Core.Height == 0
                || this.Core.FillHeightMax == 0
                || (this.Core.HasKonus && (((this.Core.OuterKonusMin ?? 0) == 0) || ((this.Core.OuterKonusMax ?? 0) == 0) || ((this.Core.OuterKonusAngle ?? 0) == 0) || ((this.Core.KonusHeight ?? 0) == 0)))
                || (this.Core.HasHoleguide && ((this.Core.AdapterDiameter ?? 0) == 0)))
            {
                return false;
            }

            return true;
        }
    }
}
