//-----------------------------------------------------------------------
// <copyright file="Components_BoltViewModel.cs" company="PlaceholderCompany">
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

    class Components_BoltViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Components_BoltViewModel"/> class.
        /// </summary>
        public Components_BoltViewModel()
        {
            this.Bolt = new Bolt() { HasThread = true };
            this.InsertIntoDbCmd = new RelayCommand(param => this.InsertIntoDb(), param => this.ValidateInput());
        }

        public Bolt Bolt { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

        public void InsertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.Bolts.Add(this.Bolt);
                    db.SaveChanges();
                    MessageBox.Show("Bolzen erfolgreich hinzugefügt");
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
            if (this.Bolt.ID.ToString().Length <= 1
                || ((this.Bolt.OuterDiameter ?? 0) == 0)
                || this.Bolt.Height == 0
                || (this.Bolt.HasThread && string.IsNullOrWhiteSpace(this.Bolt.Thread))
                || this.Bolt.HasGuideBolt && (((this.Bolt.GuideHeight ?? 0) == 0) || ((this.Bolt.GuideOuterDiameter ?? 0) == 0)))
            {
                return false;
            }

            return true;
        }
    }
}
