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
        public Bolt Bolt { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

        public Components_BoltViewModel()
        {
            this.Bolt = new Bolt() { HasThread = true };
            InsertIntoDbCmd = new RelayCommand(param => InsertIntoDb(), param => ValidateInput());
        }

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

        public bool ValidateInput()
        {
            if (Bolt.ID.ToString().Length <= 1
                || ((Bolt.OuterDiameter ?? 0) == 0)
                || Bolt.Height == 0
                || (Bolt.HasThread && string.IsNullOrWhiteSpace(Bolt.Thread))
                || Bolt.HasGuideBolt && (((Bolt.GuideHeight ?? 0) == 0) || ((Bolt.GuideOuterDiameter ?? 0) == 0)))
            {
                return false;
            }

            return true;
        }
    }
}
