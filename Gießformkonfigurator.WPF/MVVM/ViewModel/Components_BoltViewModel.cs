//-----------------------------------------------------------------------
// <copyright file="Components_BoltViewModel.cs" company="PlaceholderCompany">
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
    class Components_BoltViewModel : ObservableObject
    {
        public Bolt bolt { get; set; }

        public ICommand insertIntoDbCmd { get; set; }

        public Components_BoltViewModel()
        {
            this.bolt = new Bolt();
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateInput());
        }

        public void insertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.Bolts.Add(this.bolt);
                    db.SaveChanges();
                    MessageBox.Show("Grundplatte erfolgreich hinzugefügt.");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e + "Fehler beim Hinzufügen.");
                }

            }
        }

        public bool validateInput()
        {
            if (bolt.ID.ToString().Length <= 1
                || ((bolt.OuterDiameter ?? 0) == 0)
                || ((bolt.Height == 0)
                || (bolt.HasThread && bolt.Thread == null)
                || (bolt.HasGuideBolt && ((bolt.GuideHeight ?? 0) == 0) || ((bolt.GuideOuterDiameter ?? 0) == 0))))
            {
                return false;
            }

            return true;
        }
    }
}
